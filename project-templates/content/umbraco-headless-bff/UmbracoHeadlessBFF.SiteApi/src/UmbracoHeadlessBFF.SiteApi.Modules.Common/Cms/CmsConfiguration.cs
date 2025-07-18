﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.Links;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.Preview;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms;

public static class CmsConfiguration
{
    public static void AddCmsCommonModule(this WebApplicationBuilder builder)
    {
        // Site Resolution
        builder.Services.AddTransient<SiteResolutionContext>();
        builder.Services.AddTransient<SiteResolutionMiddleware>();
        builder.Services.AddTransient<SiteResolutionService>();

        // Links
        builder.Services.AddTransient<LinkService>();

        // Preview
        builder.Services.AddTransient<PreviewMiddleware>();
    }

    public static void UseCmsCommonModuleMiddleware(this WebApplication app)
    {
        app.UseMiddleware<SiteResolutionMiddleware>();
        app.UseMiddleware<PreviewMiddleware>();
    }
}
