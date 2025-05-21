using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Urls;

public static class UrlsConfiguration
{
    public static void AddUrls(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ApplicationUrlOptions>(builder.Configuration.GetSection(ApplicationUrlOptions.SectionName));
    }
}
