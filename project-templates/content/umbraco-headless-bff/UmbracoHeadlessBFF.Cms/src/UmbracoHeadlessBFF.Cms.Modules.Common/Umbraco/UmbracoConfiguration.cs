using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Umbraco.Cms.Core.DeliveryApi;
using UmbracoHeadlessBFF.Cms.Modules.Common.Umbraco.Overrides;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Umbraco;

public static class UmbracoConfiguration
{
    extension(WebApplicationBuilder builder)
    {
        public void AddUmbracoOverrides()
        {
            builder.Services.RemoveAll<IApiRichTextMarkupParser>();
            builder.Services.AddSingleton<IApiRichTextMarkupParser, ApiRichTextMarkupParser>();
        }
    }
}
