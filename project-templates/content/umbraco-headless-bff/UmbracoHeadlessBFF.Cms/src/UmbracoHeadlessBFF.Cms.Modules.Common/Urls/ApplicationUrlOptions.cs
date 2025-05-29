using UmbracoHeadlessBFF.SharedModules.Common.Urls;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Urls;

public sealed record ApplicationUrlOptions : IApplicationUrlOptions
{
    public required string Media { get; init; }
}
