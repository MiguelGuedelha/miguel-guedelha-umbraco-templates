using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SharedModules.Common.Urls;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Urls;

public static class UrlsConfiguration
{
    extension(WebApplicationBuilder builder)
    {
        public void AddUrlsModule()
        {
            builder.Services.Configure<ApplicationUrlOptions>(builder.Configuration.GetSection(IApplicationUrlOptions.SectionName));
        }
    }
}
