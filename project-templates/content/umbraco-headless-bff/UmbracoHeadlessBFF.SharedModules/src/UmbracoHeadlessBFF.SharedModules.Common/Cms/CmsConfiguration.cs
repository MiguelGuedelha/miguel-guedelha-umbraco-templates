using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Clients;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Clients.Handlers;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.Options;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms;

public static class CmsConfiguration
{
    public static void AddCms(this WebApplicationBuilder builder)
    {
        var refitSettings = new RefitSettings
        {
            CollectionFormat = CollectionFormat.Multi
        };

        builder.Services.Configure<CmsServiceOptions>(builder.Configuration.GetSection(CmsServiceOptions.SectionName));
        builder.Services.AddTransient<DeliveryApiHeadersHandler>();
        builder.Services.AddRefitClient<IUmbracoDeliveryApi>(refitSettings)
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new("https://Cms/umbraco/delivery/api/v2");
            })
            .AddHttpMessageHandler<DeliveryApiHeadersHandler>()
            .AddHeaderPropagation();
    }
}
