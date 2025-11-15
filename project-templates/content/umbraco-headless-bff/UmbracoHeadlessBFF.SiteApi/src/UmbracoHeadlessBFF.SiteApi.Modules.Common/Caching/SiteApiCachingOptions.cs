using UmbracoHeadlessBFF.SharedModules.Common.Caching;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching;

public sealed record SiteApiCachingOptions : DefaultCachingOptions
{
    public new required DefaultRegion Default { get; init; }
}
