using Microsoft.Extensions.Hosting;

namespace UmbracoBFFAstro.SharedModules.Features.Environment;

public static class EnvironmentExtensions
{
    public static bool IsLocal(this IHostEnvironment environment) => environment.IsEnvironment("Local");
}
