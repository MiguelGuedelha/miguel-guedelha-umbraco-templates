using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.Preview;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms;

public static class CmsConfiguration
{
    public static void AddCms(this WebApplicationBuilder builder)
    {
        // Site Resolution
        builder.Services.AddScoped<SiteResolutionContext>();
        builder.Services.AddScoped<SiteResolutionMiddleware>();
        builder.Services.AddScoped<SiteResolutionService>();

        // Preview
        builder.Services.AddScoped<PreviewMiddleware>();
    }

    public static void UseCms(this WebApplication app)
    {
        app.UseMiddleware<SiteResolutionMiddleware>();
        app.UseMiddleware<PreviewMiddleware>();
    }
}
