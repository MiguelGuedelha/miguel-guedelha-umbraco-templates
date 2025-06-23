using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SharedModules.Common.Urls;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Configuration;

public static class UrlsConfiguration
{
    public static void AddConfigurationCommonModule(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ApplicationUrlOptions>(builder.Configuration.GetSection(IApplicationUrlOptions.SectionName));
    }
}
