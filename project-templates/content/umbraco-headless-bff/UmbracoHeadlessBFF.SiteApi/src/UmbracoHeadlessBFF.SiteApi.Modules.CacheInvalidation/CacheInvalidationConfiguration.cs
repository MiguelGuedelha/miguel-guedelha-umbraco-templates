using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UmbracoHeadlessBFF.SharedModules.Common.ServiceDiscovery;

namespace UmbracoHeadlessBFF.SiteApi.Modules.CacheInvalidation;

public static class CacheInvalidationConfiguration
{
    extension(WebApplicationBuilder builder)
    {
        public void AddCacheInvalidationModule()
        {
            builder.AddAzureServiceBusClient(Services.ServiceBus.Name);
            builder.Services.AddHostedService<CacheInvalidationBackgroundService>();
        }
    }

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
