using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Links;

public static class LinksConfiguration
{
    public static void AddLinks(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<LinkService>();
    }
}
