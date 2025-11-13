using UmbracoHeadlessBFF.SharedModules.Common.Urls;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Configuration;

public sealed record ApplicationUrlOptions : IApplicationUrlOptions
{
    public required string Media { get; init; }
}
