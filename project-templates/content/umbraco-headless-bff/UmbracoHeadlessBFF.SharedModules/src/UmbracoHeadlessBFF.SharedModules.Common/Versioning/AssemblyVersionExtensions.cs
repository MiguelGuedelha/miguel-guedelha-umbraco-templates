using System.Reflection;

namespace UmbracoHeadlessBFF.SharedModules.Common.Versioning;

public static class AssemblyVersionExtensions
{
    public static string GetVersion()
    {
        var assemblyInfo = Assembly
            .GetEntryAssembly()
            ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion;

        return string.IsNullOrWhiteSpace(assemblyInfo) ? "1.0.0" : assemblyInfo;
    }
}
