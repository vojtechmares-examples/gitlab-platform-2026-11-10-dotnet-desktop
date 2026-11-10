using System.Reflection;
using System.Runtime.InteropServices;

namespace HelloDesktop.Core;

/// <summary>Tells which build of the application runs on which system.</summary>
public static class PlatformInfo
{
    public static string AppVersion =>
        typeof(PlatformInfo).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.Split('+')[0]
        ?? "0.0.0";

    public static string OperatingSystem =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Windows"
        : RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? "macOS"
        : RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Linux"
        : "unknown";

    public static string Describe() =>
        $"HelloDesktop {AppVersion} on {OperatingSystem} ({RuntimeInformation.OSArchitecture.ToString().ToLowerInvariant()}), {RuntimeInformation.FrameworkDescription}";
}
