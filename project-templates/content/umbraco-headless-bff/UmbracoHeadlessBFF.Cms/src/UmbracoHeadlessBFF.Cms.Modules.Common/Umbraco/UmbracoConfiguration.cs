using Microsoft.AspNetCore.Builder;
using Umbraco.Cms.Core.DeliveryApi;
using Umbraco.Extensions;
using UmbracoHeadlessBFF.Cms.Modules.Common.Umbraco.Overrides.RichText;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Umbraco;

public static class UmbracoConfiguration
{
    extension(WebApplicationBuilder builder)
    {
        public void AddUmbracoOverrides()
        {
            builder.Services.AddUnique<IApiRichTextMarkupParser, ApiRichTextMarkupParser>();
            builder.Services.AddUnique<IApiRichTextElementParser, ApiRichTextElementParser>();
        }
    }
}
