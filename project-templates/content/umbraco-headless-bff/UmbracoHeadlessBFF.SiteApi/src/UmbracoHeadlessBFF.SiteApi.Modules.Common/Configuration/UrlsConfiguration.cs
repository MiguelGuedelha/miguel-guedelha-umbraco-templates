using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SharedModules.Common.Urls;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Configuration;

public static class UrlsConfiguration
{
    extension(WebApplicationBuilder builder)
    {
        public void AddConfigurationCommonModule()
        {
            builder.Services.Configure<ApplicationUrlOptions>(builder.Configuration.GetSection(IApplicationUrlOptions.SectionName));
        }
    }
}
