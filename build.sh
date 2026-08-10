#!/usr/bin/env bash
set -euo pipefail

root="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd -P)"
dotnet pack "$root/Legato.csproj" --configuration Release --output "$root/artifacts/packages" "$@"
dotnet pack "$root/Tool/Legato.Tool.csproj" --configuration Release --output "$root/artifacts/packages" "$@"
