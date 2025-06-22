using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Caching;

public static class CachingConfiguration
{
    public static void AddCachingModule(this WebApplicationBuilder builder)
    {
        builder.AddAzureServiceBusClient("ServiceBus");
        builder.Services.AddHostedService<CacheInvalidationBackgroundService>();
    }

    public static void MapCachingEndpoints(this RouteGroupBuilder apiVersionGroup)
    {
        apiVersionGroup
            .MapGroup("/caching")
            .WithTags("Caching")
            .MapDeleteCacheByTag()
            .MapDeleteCacheByTags();
    }
}
