using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UmbracoHeadlessBFF.SharedModules.Common.ServiceDiscovery;

namespace UmbracoHeadlessBFF.SiteApi.Modules.CacheInvalidation;

public static class CacheInvalidationConfiguration
{
    public static void AddCacheInvalidationModule(this WebApplicationBuilder builder)
    {
        builder.AddAzureServiceBusClient(Services.ServiceBus.Name);
        builder.Services.AddHostedService<CacheInvalidationBackgroundService>();
    }

    public static void MapCacheInvalidationEndpoints(this RouteGroupBuilder apiVersionGroup)
    {
        apiVersionGroup
            .MapGroup("/caching")
            .WithTags("Caching")
            .MapDeleteCacheByTag()
            .MapDeleteCacheByTags();
    }
}
