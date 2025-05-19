using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Links;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.OEmbed;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Media;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Converters;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Endpoints;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks.Media;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Components;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks.Media;

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
            .AddTransient<IMapper<IEnumerable<ApiCard>, IReadOnlyCollection<Card>>, CardMapper>()
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
            .AddTransient<IComponentMapper, GalleryMapper>()
            .AddTransient<IComponentMapper, RichTextSectionMapper>()
            .AddTransient<IComponentMapper, SectionHeaderMapper>()
            .AddTransient<IComponentMapper, FullWidthImageMapper>()
            .AddTransient<IComponentMapper, CarouselMapper>()
            .AddTransient<IComponentMapper, BannerMapper>();

        services.AddTransient<IComponentMapper, FallbackComponentMapper>();

        // Page Mappers
    }

    public static void AddContentConverters(this IList<JsonConverter> convertersList)
    {
        convertersList.Add(new ComponentConverter());
        convertersList.Add(new MediaBlockConverter());
    }

    public static void MapContentEndpoints(this RouteGroupBuilder apiVersionGroup)
    {
        apiVersionGroup
            .MapGroup("/content")
            .WithTags("Content")
            .MapGetContent();
    }
}
