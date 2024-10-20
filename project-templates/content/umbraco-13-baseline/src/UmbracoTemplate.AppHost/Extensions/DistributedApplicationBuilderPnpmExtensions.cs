using Microsoft.Extensions.Hosting;

namespace UmbracoTemplate.AppHost.Extensions;

public static class DistributedApplicationBuilderPnpmExtensions
{
    public static IResourceBuilder<NodeAppResource> AddPnpmApp(this IDistributedApplicationBuilder builder, string name,
        string workingDirectory, string scriptName = "start", string[]? args = null)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(workingDirectory);
        ArgumentNullException.ThrowIfNull(scriptName);

        string[] allArgs = args is { Length: > 0 }
            ? ["run", scriptName, "--", .. args]
            : ["run", scriptName];

        var path = Path.Combine(builder.AppHostDirectory, workingDirectory);

        workingDirectory = path switch
        {
            _ when string.IsNullOrEmpty(path) => path,
            _ => Path.GetFullPath(path.Replace('\\', Path.DirectorySeparatorChar)
                .Replace('/', Path.DirectorySeparatorChar))
        };

        var resource = new NodeAppResource(name, "pnpm", workingDirectory);

        return builder.AddResource(resource)
            .WithNodeDefaults()
            .WithArgs(allArgs);
    }

    private static IResourceBuilder<NodeAppResource> WithNodeDefaults(this IResourceBuilder<NodeAppResource> builder) =>
        builder.WithOtlpExporter()
            .WithEnvironment("NODE_ENV", builder.ApplicationBuilder.Environment.IsDevelopment() ? "development" : "production");
}
