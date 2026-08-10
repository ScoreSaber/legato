using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Xml.Linq;

namespace Legato.Tool;

internal sealed record InstallOptions(
    string? Project,
    string? SourceVersion,
    string? TargetVersion,
    string PackageVersion,
    string? PackageSource,
    bool NoRestore);

internal static class Installer {
    private const string PackageId = "LegatoBS";

    private static readonly HashSet<string> IgnoredDirectories = new(StringComparer.OrdinalIgnoreCase) {
        ".git", ".legato", ".vs", "artifacts", "bin", "node_modules", "obj", "packages", "refs", "tools"
    };

    internal static async Task Install(InstallOptions options) {
        (string projectPath, string root) = FindProject(options.Project);
        string? buildPropsPath = FindFile(Path.GetDirectoryName(projectPath)!, root, "Directory.Build.props");
        var propertyDocuments = new List<XDocument> { XDocument.Load(projectPath) };
        if (buildPropsPath != null) {
            propertyDocuments.Add(XDocument.Load(buildPropsPath));
        }

        string? ReadProperty(string name) {
            foreach (XDocument document in propertyDocuments) {
                string? value = document.Descendants()
                    .LastOrDefault(element =>
                        element.Name.LocalName == name
                        && element.Attribute("Condition") == null
                        && element.Parent?.Attribute("Condition") == null)
                    ?.Value.Trim();
                if (value != null && !value.Contains("$(", StringComparison.Ordinal)) {
                    return value;
                }
            }
            return null;
        }

        string? detectedVersion = ReadProperty("GameVersion");
        if (detectedVersion == null) {
            string manifestPath = Path.Combine(Path.GetDirectoryName(projectPath)!, "manifest.json");
            if (File.Exists(manifestPath)) {
                using JsonDocument manifest = JsonDocument.Parse(File.ReadAllText(manifestPath));
                if (manifest.RootElement.TryGetProperty("gameVersion", out JsonElement gameVersion)) {
                    detectedVersion = gameVersion.GetString();
                }
            }
        }

        string? sourceVersion = options.SourceVersion ?? ReadProperty("LegatoSourceVersion") ?? detectedVersion;
        string? targetVersion = options.TargetVersion ?? ReadProperty("LegatoTargetVersion") ?? detectedVersion;
        sourceVersion ??= targetVersion;
        targetVersion ??= sourceVersion;
        if (sourceVersion == null || targetVersion == null) {
            throw new InvalidOperationException("Could not infer the source and target versions; pass both explicitly");
        }

        using Stream versionsFile = Assembly.GetExecutingAssembly().GetManifestResourceStream("Legato.Versions.props")
            ?? throw new InvalidOperationException("Legato's supported version list is missing");
        HashSet<string> supportedVersions = XDocument.Load(versionsFile)
            .Descendants()
            .Where(element => element.Name.LocalName == "LegatoGameVersion")
            .SelectMany(element => (element.Attribute("Include")?.Value ?? string.Empty).Split(';'))
            .Where(version => version.Length > 0)
            .ToHashSet(StringComparer.Ordinal);
        if (!supportedVersions.Contains(sourceVersion)) {
            throw new InvalidOperationException($"Beat Saber {sourceVersion} is not a supported Legato source version");
        }
        if (!supportedVersions.Contains(targetVersion)) {
            throw new InvalidOperationException($"Beat Saber {targetVersion} is not a supported Legato target version");
        }

        string? centralPackagesPath = FindFile(Path.GetDirectoryName(projectPath)!, root, "Directory.Packages.props");
        byte[] projectBefore = File.ReadAllBytes(projectPath);
        byte[]? centralPackagesBefore = centralPackagesPath == null ? null : File.ReadAllBytes(centralPackagesPath);

        try {
            await RunDotnet(root, "add", projectPath, "package", PackageId, "--version", options.PackageVersion, "--no-restore");
            UpdateProject(projectPath, centralPackagesPath, options.PackageVersion, sourceVersion, targetVersion);

            if (!options.NoRestore) {
                var restoreArguments = new List<string> { "restore", projectPath };
                if (options.PackageSource != null) {
                    string packageSource = Uri.TryCreate(options.PackageSource, UriKind.Absolute, out Uri? sourceUri) && !sourceUri.IsFile
                        ? options.PackageSource
                        : Path.GetFullPath(options.PackageSource, root);
                    restoreArguments.Add($"-p:RestoreAdditionalProjectSources={packageSource}");
                }
                await RunDotnet(root, restoreArguments.ToArray());
            }
        } catch {
            File.WriteAllBytes(projectPath, projectBefore);
            if (centralPackagesPath != null && centralPackagesBefore != null) {
                File.WriteAllBytes(centralPackagesPath, centralPackagesBefore);
            }
            throw;
        }

        Console.WriteLine($"Installed Legato {options.PackageVersion} in {Path.GetRelativePath(root, projectPath)}");
        Console.WriteLine($"Source API: {sourceVersion}");
        Console.WriteLine($"Target game: {targetVersion}");
    }

    private static async Task RunDotnet(string workingDirectory, params string[] arguments) {
        var startInfo = new ProcessStartInfo("dotnet") {
            WorkingDirectory = workingDirectory,
            UseShellExecute = false
        };
        foreach (string argument in arguments) {
            startInfo.ArgumentList.Add(argument);
        }

        using Process process = Process.Start(startInfo)
            ?? throw new InvalidOperationException("Could not start dotnet");
        await process.WaitForExitAsync();
        if (process.ExitCode != 0) {
            throw new InvalidOperationException($"dotnet {arguments[0]} failed with exit code {process.ExitCode}");
        }
    }

    private static void UpdateProject(string projectPath, string? centralPackagesPath, string packageVersion, string sourceVersion, string targetVersion) {
        XDocument project = XDocument.Load(projectPath);
        XElement root = project.Root ?? throw new InvalidOperationException("The project file has no root element");
        XElement packageReference = project.Descendants().FirstOrDefault(element =>
                element.Name.LocalName == "PackageReference"
                && string.Equals(element.Attribute("Include")?.Value ?? element.Attribute("Update")?.Value, PackageId, StringComparison.OrdinalIgnoreCase))
            ?? throw new InvalidOperationException("dotnet did not add the Legato package reference");
        packageReference.SetAttributeValue("PrivateAssets", "all");
        packageReference.Elements().Where(element => element.Name.LocalName == "PrivateAssets").Remove();

        if (centralPackagesPath != null) {
            XDocument centralPackages = XDocument.Load(centralPackagesPath);
            if (centralPackages.Descendants().Any(element =>
                    element.Name.LocalName == "ManagePackageVersionsCentrally"
                    && element.Value.Equals("true", StringComparison.OrdinalIgnoreCase))) {
                packageReference.Attribute("Version")?.Remove();
                packageReference.Elements().Where(element => element.Name.LocalName == "Version").Remove();

                XElement centralRoot = centralPackages.Root
                    ?? throw new InvalidOperationException("Directory.Packages.props has no root element");
                XElement? packageVersionElement = centralPackages.Descendants().FirstOrDefault(element =>
                    element.Name.LocalName == "PackageVersion"
                    && string.Equals(element.Attribute("Include")?.Value ?? element.Attribute("Update")?.Value, PackageId, StringComparison.OrdinalIgnoreCase));
                if (packageVersionElement == null) {
                    XElement group = centralRoot.Elements().FirstOrDefault(element =>
                            element.Name.LocalName == "ItemGroup" && element.Attribute("Condition") == null)
                        ?? new XElement(centralRoot.Name.Namespace + "ItemGroup");
                    if (group.Parent == null) {
                        centralRoot.Add(group);
                    }
                    packageVersionElement = new XElement(centralRoot.Name.Namespace + "PackageVersion", new XAttribute("Include", PackageId));
                    group.Add(packageVersionElement);
                }
                packageVersionElement.SetAttributeValue("Version", packageVersion);
                packageVersionElement.Elements().Where(element => element.Name.LocalName == "Version").Remove();
                centralPackages.Save(centralPackagesPath);
            }
        }

        SetProperty(root, "LegatoSourceVersion", sourceVersion);
        SetProperty(root, "LegatoTargetVersion", targetVersion);
        project.Save(projectPath);
    }

    private static void SetProperty(XElement root, string name, string value) {
        XElement[] existing = root.Descendants().Where(element => element.Name.LocalName == name).ToArray();
        if (existing.Length > 1 || existing.Any(element => element.Attribute("Condition") != null || element.Parent?.Attribute("Condition") != null)) {
            throw new InvalidOperationException($"{name} is conditional or defined more than once; set it manually");
        }
        if (existing.Length == 1) {
            existing[0].Value = value;
            return;
        }

        XElement group = root.Elements().FirstOrDefault(element =>
                element.Name.LocalName == "PropertyGroup" && element.Attribute("Condition") == null)
            ?? new XElement(root.Name.Namespace + "PropertyGroup");
        if (group.Parent == null) {
            root.AddFirst(group);
        }
        group.Add(new XElement(root.Name.Namespace + name, value));
    }

    private static (string Path, string Root) FindProject(string? requestedPath) {
        string path = Path.GetFullPath(requestedPath ?? Environment.CurrentDirectory);
        string projectPath;
        if (File.Exists(path)) {
            if (!Path.GetExtension(path).Equals(".csproj", StringComparison.OrdinalIgnoreCase)) {
                throw new InvalidOperationException($"Not a C# project: {path}");
            }
            projectPath = path;
        } else {
            if (!Directory.Exists(path)) {
                throw new InvalidOperationException($"Path does not exist: {path}");
            }

            var projects = new List<string>();
            var pending = new Stack<string>();
            pending.Push(path);
            while (pending.TryPop(out string? directory)) {
                projects.AddRange(Directory.EnumerateFiles(directory, "*.csproj"));
                foreach (string child in Directory.EnumerateDirectories(directory)) {
                    var info = new DirectoryInfo(child);
                    if (!IgnoredDirectories.Contains(info.Name) && !info.Attributes.HasFlag(FileAttributes.ReparsePoint)) {
                        pending.Push(child);
                    }
                }
            }

            projects.Sort(StringComparer.OrdinalIgnoreCase);
            if (projects.Count == 0) {
                throw new InvalidOperationException($"No C# project found under {path}");
            }
            if (projects.Count > 1) {
                string choices = string.Join(Environment.NewLine, projects.Select(project => $"  {Path.GetRelativePath(path, project)}"));
                throw new InvalidOperationException($"Multiple C# projects found; pass the mod project explicitly:{Environment.NewLine}{choices}");
            }
            projectPath = projects[0];
        }

        for (DirectoryInfo? directory = Directory.GetParent(projectPath); directory != null; directory = directory.Parent) {
            if (Path.Exists(Path.Combine(directory.FullName, ".git"))) {
                return (projectPath, directory.FullName);
            }
        }
        return (projectPath, Path.GetDirectoryName(projectPath)!);
    }

    private static string? FindFile(string start, string stop, string fileName) {
        string boundary = Path.GetFullPath(stop).TrimEnd(Path.DirectorySeparatorChar);
        for (string? directory = Path.GetFullPath(start); directory != null; directory = Directory.GetParent(directory)?.FullName) {
            string candidate = Path.Combine(directory, fileName);
            if (File.Exists(candidate)) {
                return candidate;
            }
            if (directory.Equals(boundary, StringComparison.OrdinalIgnoreCase)) {
                return null;
            }
        }
        return null;
    }
}
