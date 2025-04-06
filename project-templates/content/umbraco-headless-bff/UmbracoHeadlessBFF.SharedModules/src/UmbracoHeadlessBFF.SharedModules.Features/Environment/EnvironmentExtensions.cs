// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting;

public static class EnvironmentExtensions
{
    public static bool IsLocal(this IHostEnvironment environment) => environment.IsEnvironment("Local");
}
