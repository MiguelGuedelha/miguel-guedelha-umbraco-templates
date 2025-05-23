using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SharedModules.Common.Urls;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Urls;

public static class UrlsConfiguration
{
    public static void AddUrls(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ApplicationUrlOptions>(builder.Configuration.GetSection(IApplicationUrlOptions.SectionName));
    }
}
