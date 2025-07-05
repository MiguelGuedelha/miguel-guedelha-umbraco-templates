using System.Reflection;

namespace UmbracoHeadlessBFF.SharedModules.Common.Versioning;

public static class AssemblyVersionExtensions
{
    public static string GetVersion()
    {
        return Assembly
            .GetEntryAssembly()
            ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion ?? "1.0.0";
    }
}
