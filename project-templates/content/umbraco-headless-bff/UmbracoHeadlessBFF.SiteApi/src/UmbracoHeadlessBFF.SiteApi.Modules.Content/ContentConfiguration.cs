using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Converters;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Endpoints;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Components;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Layouts;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Pages;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content;

public static class ContentConfiguration
{
    public static void AddContent(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddTransient<IContentService, ContentService>();

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
            .AddTransient<IResponsiveImageMapper, ResponsiveImageMapper>()
            .AddTransient<INavigationLinkMapper, NavigationLinkMapper>()
            .AddTransient<ILinkWithSublinksMapper, LinkWithSublinksMapper>()
            .AddTransient<IHeadingWithLinksMapper, HeadingWithLinksMapper>()
            .AddTransient<IHeadingWithSocialLinksMapper, HeadingWithSocialLinksMapper>();

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
            .AddTransient<ISeoMapper, SeoMapper>()
            .AddTransient<ISiteSettingsMapper, SiteSettingsMapper>();

        // Page Mappers
        services
            .AddTransient<IPageMapper, HomeMapper>()
            .AddTransient<IPageMapper, SiteSearchMapper>()
            .AddTransient<IPageMapper, StandardContentMapper>()
            .AddTransient<IPageMapper, BlogArticleMapper>()
            .AddTransient<IPageMapper, NotFoundMapper>();

        services.AddTransient<IPageMapper, FallbackPageMapper>();
    }

    public static void AddContentConverters(this IList<JsonConverter> convertersList)
    {
        convertersList.Add(new ComponentConverter());
        convertersList.Add(new MediaBlockConverter());
        convertersList.Add(new LayoutConverter());
        convertersList.Add(new PageConverter());
    }

    public static void MapContentEndpoints(this RouteGroupBuilder apiVersionGroup)
    {
        apiVersionGroup
            .MapGroup("/content")
            .WithTags("Content")
            .MapGetPageByIdOrPath()
            .MapGetNotFoundPage()
            .MapGetSitemap()
            .MapGetDictionary();
    }
}
