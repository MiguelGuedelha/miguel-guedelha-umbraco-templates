using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace UmbracoHeadlessBFF.SiteApi.Modules.CacheInvalidation;

public static class CacheInvalidationConfiguration
{

    extension(RouteGroupBuilder builder)
    {
        public void MapCacheInvalidationEndpoints()
        {
            builder
                .MapGroup("/caching")
                .WithTags("Caching")
                .MapDeleteCacheByTag()
                .MapDeleteCacheByTags();
        }
    }
}
