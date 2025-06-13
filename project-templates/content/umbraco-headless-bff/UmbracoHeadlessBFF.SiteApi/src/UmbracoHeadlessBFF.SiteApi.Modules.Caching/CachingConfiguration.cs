using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UmbracoHeadlessBFF.SiteApi.Modules.Caching.DeleteCacheWebhook;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Caching;

public static class CachingConfiguration
{
    public static void MapCachingEndpoints(this RouteGroupBuilder apiVersionGroup)
    {
        apiVersionGroup
            .MapGroup("/caching")
            .WithTags("Caching")
            .MapDeleteCacheByTag()
            .MapDeleteCacheByTags()
            .DeleteCacheWebhook();
    }
}
