using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Links;

public static class LinksConfiguration
{
    extension(WebApplicationBuilder builder)
    {
        public void AddLinksModule()
        {
            builder.Services.AddTransient<LinkService>();
        }
    }
}
