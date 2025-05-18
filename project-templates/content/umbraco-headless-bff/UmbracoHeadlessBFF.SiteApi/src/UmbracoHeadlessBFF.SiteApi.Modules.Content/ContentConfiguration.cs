using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Endpoints;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content;

public static class ContentConfiguration
{
    public static void AddContent(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ContentService>();

        // Building Block Mappers
        builder.Services.AddTransient<LinkMapper>();
        builder.Services.AddTransient<ResponsiveImageMapper>();

        // Component Mappers
        builder.Services.AddTransient<IComponentMapper, SpotlightMapper>();
        builder.Services.AddTransient<IComponentMapper, NullComponentMapper>();

        // Page Mappers
    }

    public static void MapContentEndpoints(this RouteGroupBuilder apiVersionGroup)
    {
        apiVersionGroup
            .MapGroup("/content")
            .WithTags("Content")
            .MapGetContent();
    }
}
