using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Clients;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Clients.Handlers;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Converters;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.Options;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms;

public static class CmsConfiguration
{
    private static readonly RefitSettings s_clientSettings = new()
    {
        CollectionFormat = CollectionFormat.Multi,
        ContentSerializer = new SystemTextJsonContentSerializer(new()
        {
            Converters =
            {
                new JsonStringEnumConverter(),
                new ApiElementConverter(),
                new ApiContentConverter()
            }
        })
    };

    public static void AddCms(this WebApplicationBuilder builder)
    {

        builder.Services.Configure<CmsServiceOptions>(builder.Configuration.GetSection(CmsServiceOptions.SectionName));
        builder.Services.AddTransient<DeliveryApiHeadersHandler>();
        builder.Services.AddRefitClient<IUmbracoDeliveryApi>(s_clientSettings)
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new("https://Cms/umbraco/delivery/api/v2");
            })
            .AddHttpMessageHandler<DeliveryApiHeadersHandler>()
            .AddHeaderPropagation();
    }
}
