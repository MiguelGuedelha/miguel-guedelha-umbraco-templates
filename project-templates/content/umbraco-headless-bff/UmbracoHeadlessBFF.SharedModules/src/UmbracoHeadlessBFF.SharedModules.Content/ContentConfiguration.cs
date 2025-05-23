using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Content.Sitemaps;

namespace UmbracoHeadlessBFF.SharedModules.Content;

public static class ContentConfiguration
{
    public static void AddContentSharedModules(this WebApplicationBuilder builder)
    {
        builder.Services.AddRefitClient<ISitemapsApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new("https://Cms/api/v1.0/content");
            })
            .AddHttpMessageHandler<DeliveryApiHeadersHandler>()
            .AddHeaderPropagation();
    }
}
