using System.Reflection;
using Legato.Tool;

try {
    if (args.Length == 0 || args[0] is "-h" or "--help") {
        PrintHelp();
        return 0;
    }
    if (args[0] != "install") {
        throw new InvalidOperationException($"Unknown command '{args[0]}'. Run 'legato --help' for usage");
    }

    string packageVersion = Assembly.GetExecutingAssembly()
        .GetCustomAttributes<AssemblyMetadataAttribute>()
        .Single(attribute => attribute.Key == "LegatoPackageVersion")
        .Value ?? throw new InvalidOperationException("Legato's package version is missing");
    string? project = null;
    string? sourceVersion = null;
    string? targetVersion = null;
    string? packageSource = null;
    bool noRestore = false;

    for (int index = 1; index < args.Length; index++) {
        string argument = args[index];
        switch (argument) {
            case "-h":
            case "--help":
                PrintHelp();
                return 0;
            case "--source-version":
                sourceVersion = ReadValue(args, ref index, argument);
                break;
            case "--target-version":
                targetVersion = ReadValue(args, ref index, argument);
                break;
            case "--package-version":
                packageVersion = ReadValue(args, ref index, argument);
                break;
            case "--source":
                packageSource = ReadValue(args, ref index, argument);
                break;
            case "--no-restore":
                noRestore = true;
                break;
            default:
                if (argument.StartsWith('-')) {
                    throw new InvalidOperationException($"Unknown option '{argument}'");
                }
                if (project != null) {
                    throw new InvalidOperationException("Pass only one project file or directory");
                }
                project = argument;
                break;
        }
    }

    await Installer.Install(new InstallOptions(project, sourceVersion, targetVersion, packageVersion, packageSource, noRestore));
    return 0;
} catch (Exception exception) {
    Console.Error.WriteLine($"error: {exception.Message}");
    return 1;
}

static string ReadValue(string[] arguments, ref int index, string option) {
    if (++index >= arguments.Length || string.IsNullOrWhiteSpace(arguments[index])) {
        throw new InvalidOperationException($"{option} requires a value");
    }
    return arguments[index];
}

static void PrintHelp() {
    Console.WriteLine("""
        Add Legato to a Beat Saber mod project

        Usage:
          legato install [project] [options]

        Options:
          --source-version <version>   API version the source was written against
          --target-version <version>   Beat Saber version this build targets
          --package-version <version>  Legato package version to install
          --source <path-or-url>        Additional NuGet source used during restore
          --no-restore                  Update the project without restoring packages
        """);
}
