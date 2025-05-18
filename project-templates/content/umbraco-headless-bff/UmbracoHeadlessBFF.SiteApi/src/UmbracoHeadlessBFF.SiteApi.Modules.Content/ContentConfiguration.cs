using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Links;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.OEmbed;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Media;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Endpoints;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks.MediaBlock;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Components;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content;

public static class ContentConfiguration
{
    public static void AddContent(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddTransient<ContentService>();

        // Building Block Mappers
        services
            .AddTransient<IMapper<ApiLink, Link>, LinkMapper>()
            .AddTransient<IMapper<ApiMediaWithCrops, Image>, ImageMapper>()
            .AddTransient<IMapper<ApiMediaWithCrops, Video>, VideoMapper>()
            .AddTransient<IMapper<ApiOEmbedItem, EmbedItem>, EmbedItemMapper>()
            .AddTransient<IMapper<IApiElement, IMediaBlock>, MediaBlockMapper>()
            .AddTransient<IMapper<ApiEmbedVideo, EmbedVideo>, EmbedVideoMapper>()
            .AddTransient<IMapper<ApiMediaLibraryVideo, MediaLibraryVideo>, MediaLibraryVideoMapper>()
            .AddTransient<IMapper<ApiResponsiveImage, ResponsiveImage>, ResponsiveImageMapper>();


        // Component Mappers
        services
            .AddTransient<IComponentMapper, SpotlightMapper>()
            .AddTransient<IComponentMapper, NullComponentMapper>();

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
