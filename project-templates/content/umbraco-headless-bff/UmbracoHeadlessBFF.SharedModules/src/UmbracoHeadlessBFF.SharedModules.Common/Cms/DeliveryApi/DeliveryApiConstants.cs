using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Blogs.BlogListing;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Standard;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Standard.Banner;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Standard.Gallery;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Layouts;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Blogs;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Errors;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Settings;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;

public static class DeliveryApiConstants
{
    public static class ContentTypes
    {
        public const string ApiSiteGrouping = "siteGrouping";
        public const string ApiHome = "home";
        public const string ApiStandardContentPage = "standardContentPage";
        public const string ApiSiteSearch = "siteSearch";
        public const string ApiSiteDictionary = "siteDictionary";
        public const string ApiNotFound = "notFound";
        public const string ApiBlogRepository = "blogRepository";
        public const string ApiBlogYear = "blogYear";
        public const string ApiBlogMonth = "blogMonth";
        public const string ApiBlogArticle = "blogArticle";
        public const string ApiSiteSettings = "siteSettings";

        public static readonly Dictionary<string, Type> TypesMap = new()
        {
            { ApiSiteGrouping, typeof(ApiSiteGrouping) },
            { ApiHome, typeof(ApiHome) },
            { ApiStandardContentPage, typeof(ApiStandardContentPage) },
            { ApiSiteSearch, typeof(ApiSiteSearch) },
            { ApiSiteDictionary, typeof(ApiSiteDictionary) },
            { ApiNotFound, typeof(ApiNotFound) },
            { ApiBlogRepository, typeof(ApiBlogRepository) },
            { ApiBlogYear, typeof(ApiBlogYear) },
            { ApiBlogMonth, typeof(ApiBlogMonth) },
            { ApiBlogArticle, typeof(ApiBlogArticle) },
            { ApiSiteSettings, typeof(ApiSiteSettings) },
        };
    }

    public static class ElementTypes
    {
        // Building blocks
        public const string ApiCard = "card";
        public const string ApiEmbedVideo = "embedVideo";
        public const string ApiMediaLibraryVideo = "mediaLibraryVideo";
        public const string ApiResponsiveImage = "responsiveImage";
        public const string ApiMainNavigationLink = "mainNavigationLink";
        public const string ApiLinkWithSublinks = "linkWithSublinks";
        public const string ApiHeadingWithLinks = "headingWithLinks";
        public const string ApiHeadingWithSocialLinks = "headingWithSocialLinks";

        // Components
        public const string ApiBanner = "banner";
        public const string ApiCarousel = "carousel";
        public const string ApiFullWidthImage = "fullWidthImage";
        public const string ApiGallery = "gallery";
        public const string ApiGallerySettings = "gallerySettings";
        public const string ApiRichTextSection = "richTextSection";
        public const string ApiSectionHeader = "sectionHeader";
        public const string ApiSpotlight = "spotlight";
        public const string ApiBlogListing = "blogListing";
        public const string ApiBlogListingSettings = "blogListingSettings";

        // Layouts
        public const string ApiOneColumn = "oneColumn";

        public static readonly Dictionary<string, Type> TypesMap = new()
        {
            // Building blocks
            { ApiCard, typeof(ApiCard) },
            { ApiEmbedVideo, typeof(ApiEmbedVideo) },
            { ApiMediaLibraryVideo, typeof(ApiMediaLibraryVideo) },
            { ApiResponsiveImage, typeof(ApiResponsiveImage) },
            { ApiMainNavigationLink, typeof(ApiMainNavigationLink) },
            { ApiLinkWithSublinks, typeof(ApiLinkWithSublinks) },
            { ApiHeadingWithLinks, typeof(ApiHeadingWithLinks) },
            { ApiHeadingWithSocialLinks, typeof(ApiHeadingWithSocialLinks) },

            // Components
            { ApiBanner, typeof(ApiBanner) },
            { ApiCarousel, typeof(ApiCarousel) },
            { ApiFullWidthImage, typeof(ApiFullWidthImage) },
            { ApiGallery, typeof(ApiGallery) },
            { ApiGallerySettings, typeof(ApiGallerySettings) },
            { ApiRichTextSection, typeof(ApiRichTextSection) },
            { ApiSectionHeader, typeof(ApiSectionHeader) },
            { ApiSpotlight, typeof(ApiSpotlight) },
            { ApiBlogListing, typeof(ApiBlogListing) },
            { ApiBlogListingSettings, typeof(ApiBlogListingSettings) },

            // Layouts
            { ApiOneColumn, typeof(ApiOneColumn) },
        };
    }
}
