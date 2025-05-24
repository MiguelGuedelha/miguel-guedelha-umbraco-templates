using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Converters;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.Components;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.Layouts;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.Pages;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages;

public static class PagesConfiguration
{
    public static void AddPages(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddTransient<ContentService>();

        // Building Block Mappers
        services
            .AddTransient<ILinkMapper, LinkMapper>()
            .AddTransient<ICardMapper, CardMapper>()
            .AddTransient<IImageMapper, ImageMapper>()
            .AddTransient<IVideoMapper, VideoMapper>()
            .AddTransient<IEmbedItemMapper, EmbedItemMapper>()
            .AddTransient<IMediaBlockMapper, MediaBlockMapper>()
            .AddTransient<IEmbedVideoMapper, EmbedVideoMapper>()
            .AddTransient<IMediaLibraryVideoMapper, MediaLibraryVideoMapper>()
            .AddTransient<IResponsiveImageMapper, ResponsiveImageMapper>();

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

        // LayoutMappers
        services
            .AddTransient<ILayoutMapper, OneColumnMapper>();

        services.AddTransient<ILayoutMapper, FallbackLayoutMapper>();


        // Page related mappers
        services
            .AddTransient<BasePageMapper>()
            .AddTransient<ISeoMapper, SeoMapper>();

        // Page Mappers
        services
            .AddTransient<IPageMapper, HomeMapper>()
            .AddTransient<IPageMapper, SiteSearchMapper>()
            .AddTransient<IPageMapper, StandardContentMapper>();

        services.AddTransient<IPageMapper, FallbackPageMapper>();
    }

    public static void AddPagesConverters(this IList<JsonConverter> convertersList)
    {
        convertersList.Add(new ComponentConverter());
        convertersList.Add(new MediaBlockConverter());
        convertersList.Add(new LayoutConverter());
        convertersList.Add(new PageConverter());
    }

    public static void MapPagesEndpoints(this RouteGroupBuilder apiVersionGroup)
    {
        apiVersionGroup
            .MapGroup("/content")
            .WithTags("Content")
            .MapGetPage();
    }
}
