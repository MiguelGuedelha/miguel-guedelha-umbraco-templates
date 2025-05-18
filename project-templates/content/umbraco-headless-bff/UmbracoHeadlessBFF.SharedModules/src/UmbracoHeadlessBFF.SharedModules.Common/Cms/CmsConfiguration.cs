using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Clients;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Clients.Handlers;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Converters;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.Handlers;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.Links.Clients;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.Options;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.Preview.Clients;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.SiteResolution.Clients;
using UmbracoHeadlessBFF.SharedModules.Common.Serialisation;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms;

public static class CmsConfiguration
{
    private static readonly RefitSettings s_clientSettings = new()
    {
        CollectionFormat = CollectionFormat.Multi,
        ContentSerializer = new SystemTextJsonContentSerializer(new(BaseSerializerOptions.SystemTextJsonSerializerOptions)
        {
            Converters =
            {
                new ApiElementConverter(),
                new ApiContentConverter()
            }
        })
    };

    public static void AddCmsSharedModules(this WebApplicationBuilder builder)
    {
        // Delivery Api
        builder.Services.Configure<CmsServiceOptions>(builder.Configuration.GetSection(CmsServiceOptions.SectionName));
        builder.Services.AddTransient<DeliveryApiHeadersHandler>();
        builder.Services.AddRefitClient<IUmbracoDeliveryApi>(s_clientSettings)
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new("https://Cms/umbraco/delivery/api/v2");
            })
            .AddHttpMessageHandler<DeliveryApiHeadersHandler>()
            .AddHeaderPropagation();

        // Site Resolution
        builder.Services.AddTransient<CmsApiKeyHeaderHandler>();
        builder.Services.AddRefitClient<ISiteResolutionApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new("https://Cms/api/v1.0/sites");
            })
            .AddHttpMessageHandler<CmsApiKeyHeaderHandler>()
            .AddHeaderPropagation();

        // Links
        builder.Services.AddRefitClient<ILinksApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new("https://Cms/api/v1.0/links");
            })
            .AddHttpMessageHandler<CmsApiKeyHeaderHandler>()
            .AddHeaderPropagation();

        // Preview
        builder.Services.AddRefitClient<IPreviewVerificationApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new("https://Cms/api/v1.0/preview");
            })
            .AddHeaderPropagation();
    }
}
