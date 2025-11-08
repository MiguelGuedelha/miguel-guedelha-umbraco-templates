using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using UmbracoHeadlessBFF.SharedModules.Common.ServiceDiscovery;

namespace UmbracoHeadlessBFF.Cms.Modules.Caching;

public static class CachingConfiguration
{
    extension(WebApplicationBuilder builder)
    {
        public void AddCachingModule()
        {
            builder.AddAzureServiceBusClient(Services.ServiceBus.Name);
        }
    }
}
