# [Legato](https://en.wikipedia.org/wiki/Legato)

A compatibility library for Beat Saber PC mods

This originally started out inside [ScoreSaber's PC Mod](https://github.com/ScoreSaber/pc-mod) and we decided to extract it into a re-usable library as we may need it for future projects or others may also stand to benefit. For us Legato keeps most game version differences out of the mod itself helping us focus on what's important

## Install

```sh
dotnet tool install --global LegatoBS.Tool

legato install src/MyMod.csproj \
  --source-version 1.29.0 \
  --target-version 1.44.1
```

Something like [ScoreSaber](https://github.com/ScoreSaber/pc-mod) uses the latest game as its source version then changes the target version for each build

The project path can be left out when the repo has one C# project. Versions can also be read from `GameVersion` or a `manifest.json` beside the project

Use `--source <path-or-url>` for a local package feed or `--no-restore` when the game references aren't on the current machine

The same setup can be added by hand:

```xml
<PropertyGroup>
  <LegatoSourceVersion>1.29.0</LegatoSourceVersion>
  <LegatoTargetVersion>1.44.1</LegatoTargetVersion>
</PropertyGroup>

<ItemGroup>
  <PackageReference Include="LegatoBS" Version="0.1.0" PrivateAssets="all" />
</ItemGroup>
```

## Versions

| Profile | Game versions |
| --- | --- |
| `1.29.0` | `1.29.0-1.29.1` |
| `1.37.1` | `1.37.1-1.37.2` |
| `1.38.0` | `1.38.0-1.39.1` |
| `1.40.0` | `1.40.0-1.40.8` |
| `1.42.0` | `1.42.0-1.44.1` |

## How it works

Legato compiles its source into the mod, it picks files from the source version, target version and the project's game or mod references. Support for BSML, LeaderboardCore, SiraUtil, SongCore and other mods is included when their references exist

The project needs C# 10 or later. The [library reference](LIBRARY.md) lists the adapters available to mod code

## Porting an old mod

For a mod such as SongBrowser for example which was written for 1.29.0 and hypothetically being updated to 1.44.1:

1. Run `legato install` with `1.29.0` as the source and `1.44.1` as the target
2. Update the game references, mod dependencies and manifest
3. Build the existing source and fix anything Legato doesn't cover
4. Test the build against the target
