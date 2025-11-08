using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Infrastructure.Runtime.RuntimeModeValidators;

namespace UmbracoHeadlessBFF.Cms.Web.Configuration;

/// <summary>
/// Disables checks that likely aren't needed when running in a Docker container environment (no HTTPS in container)
/// </summary>
internal sealed class DockerHostingComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (string.IsNullOrWhiteSpace(env))
        {
            throw new ArgumentException("ASPNETCORE_ENVIRONMENT not defined");
        }

        var isLocal = string.Equals(env, "local", StringComparison.OrdinalIgnoreCase);

        if (!isLocal)
        {
            builder.RuntimeModeValidators().Remove<UseHttpsValidator>();
        }
    }
}
