using UmbracoHeadlessBFF.SharedModules.Common.Versioning;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching;

public static class CachingConstants
{
    public static readonly string SiteApiCacheName = $"{AssemblyVersionExtensions.GetVersion()}:SiteApi";
    public static readonly string SiteApiOutputCacheName = $"{AssemblyVersionExtensions.GetVersion()}:SiteApi:OutputCache";
}
