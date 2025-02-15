using Microsoft.Extensions.Hosting;

// ReSharper disable once CheckNamespace
namespace Aspire.Hosting;

// Extension in Shared Modules is referenced by the main projects
// Can't import it to Aspire due to circular references
public static class EnvironmentExtensions
{
    public static bool IsLocal(this IHostEnvironment environment) => environment.IsEnvironment("Local");
}
