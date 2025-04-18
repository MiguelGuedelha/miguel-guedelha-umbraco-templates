// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting;

public static class EnvironmentExtensions
{
    //comparisons are case-insensitive
    public static bool IsLocal(this IHostEnvironment environment) => environment.IsEnvironment("Local");

    public static bool IsDev(this IHostEnvironment environment) => environment.IsEnvironment("DEV");

    public static bool IsUat(this IHostEnvironment environment) => environment.IsEnvironment("UAT");

    public static bool IsProd(this IHostEnvironment environment) => environment.IsEnvironment("PROD");
}
