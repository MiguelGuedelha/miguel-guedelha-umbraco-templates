using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UmbracoHeadlessBFF.SharedModules.Common.Caching;
using UmbracoHeadlessBFF.SharedModules.Common.ServiceDiscovery;
using ZiggyCreatures.Caching.Fusion;

namespace UmbracoHeadlessBFF.Cms.Modules.Caching;

public static class CachingConfiguration
{
    public static void AddCachingModule(this WebApplicationBuilder builder)
    {
        builder.AddAzureServiceBusClient(Services.ServiceBus.Name);
    }
}
