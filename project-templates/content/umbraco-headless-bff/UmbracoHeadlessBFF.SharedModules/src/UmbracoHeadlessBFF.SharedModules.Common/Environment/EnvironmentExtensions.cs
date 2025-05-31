using Microsoft.Extensions.Hosting;

namespace UmbracoHeadlessBFF.SharedModules.Common.Environment;

/// <summary>
/// Environment name check is case-insensitive
/// </summary>
public static class EnvironmentExtensions
{
    /// <inheritdoc cref="EnvironmentExtensions"/>
    public static bool IsLocal(this IHostEnvironment environment) => environment.IsEnvironment("local");

    /// <inheritdoc cref="EnvironmentExtensions"/>
    public static bool IsDev(this IHostEnvironment environment) => environment.IsEnvironment("dev") || environment.IsDevelopment();

    /// <inheritdoc cref="EnvironmentExtensions"/>
    public static bool IsQa(this IHostEnvironment environment) => environment.IsEnvironment("qa");

    /// <inheritdoc cref="EnvironmentExtensions"/>
    public static bool IsUat(this IHostEnvironment environment) => environment.IsEnvironment("uat");

    /// <inheritdoc cref="EnvironmentExtensions"/>
    public static bool IsStaging(this IHostEnvironment environment) => environment.IsEnvironment("stg") || environment.IsStaging();

    /// <inheritdoc cref="EnvironmentExtensions"/>
    public static bool IsProd(this IHostEnvironment environment) => environment.IsEnvironment("prod") || environment.IsProduction();
}
