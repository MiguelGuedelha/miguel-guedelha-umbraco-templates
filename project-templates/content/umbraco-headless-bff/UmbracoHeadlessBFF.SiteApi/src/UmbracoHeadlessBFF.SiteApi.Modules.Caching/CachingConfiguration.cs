using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Caching;

public static class CachingConfiguration
{
    public static void MapCachingEndpoints(this RouteGroupBuilder apiVersionGroup)
    {
        apiVersionGroup
            .MapGroup("/caching")
            .WithTags("Caching")
            .MapDeleteCacheByTag()
            .MapDeleteCacheByTags();
    }
}
