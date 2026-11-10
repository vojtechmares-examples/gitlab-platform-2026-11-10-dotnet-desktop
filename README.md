# dotnet-desktop

A small cross-platform desktop application (**Avalonia**, .NET 10) for **Windows, macOS and Linux**, built, tested and released
entirely on the Linux runner.

| Topic | How |
|-------|-----|
| Code | `HelloDesktop.Core` (logic and view model, no UI types), `HelloDesktop` (the Avalonia window), `HelloDesktop.Tests` (xUnit) |
| **CI matrix, platforms** | the `build` job runs once per runtime: `win-x64`, `osx-arm64`, `osx-x64`, `linux-x64`. .NET publishes for another system by downloading its runtime pack, so no Windows or Mac machine is involved. Output: a self-contained single file, as `.zip` (Windows) or `.tar.gz` |
| **CI matrix, .NET versions** | the `test` job runs on `mcr.microsoft.com/dotnet/sdk:9.0` and `:10.0`; the projects take their target framework from `-p:TargetFramework=net9.0` / `net10.0` |
| Coverage | coverlet + ReportGenerator; percentage on the job, Cobertura in merge requests, HTML report as an artifact |
| Security | Trivy through the Dependency Proxy scans the NuGet lock files |
| **Dependency Proxy** | used for the Docker Hub images (Trivy, `docker`, `docker:dind`). It cannot serve the .NET SDK: Microsoft publishes it on `mcr.microsoft.com`, the proxy caches Docker Hub only. The manual job `proxy:feasibility` tries both and shows the result |
| **Releases** | a tag `v1.2.3` runs `release:production`: the four files and `SHA256SUMS` go to the Generic Package Registry and are attached to a **GitLab Release** (Deploy > Releases) |
| Environments | `staging`: every build on `main` is uploaded as `1.0.0-dev.<pipeline id>` (the pre-release channel); `production`: the tagged release |

The binaries are not code-signed or notarized (a Windows certificate and an Apple developer account are needed for that).

Release: `git tag -a v1.0.0 -m "HelloDesktop 1.0.0" && git push origin v1.0.0`.
