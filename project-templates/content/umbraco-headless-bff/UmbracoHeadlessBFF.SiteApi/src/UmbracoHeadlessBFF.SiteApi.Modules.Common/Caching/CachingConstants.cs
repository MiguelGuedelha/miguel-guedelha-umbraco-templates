using UmbracoHeadlessBFF.SharedModules.Common.Versioning;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching;

public static class CachingConstants
{
    public static string SiteApiCacheName = $"{AssemblyVersionExtensions.GetVersion()}SiteApi";

}
