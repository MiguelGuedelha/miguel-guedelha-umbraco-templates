using System.Text.Json;
using System.Text.Json.Serialization;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Blogs.BlogListing;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Standard;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Standard.Banner;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Standard.Gallery;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Layouts;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Converters;

public sealed class ApiElementConverter : JsonConverter<IApiElement>
{
    private static readonly Dictionary<string, Type> s_elementMap = new()
    {
        // Building blocks
        { ApiCard.ContentType, typeof(ApiCard) },
        { ApiEmbedVideo.ContentType, typeof(ApiEmbedVideo) },
        { ApiMediaLibraryVideo.ContentType, typeof(ApiMediaLibraryVideo) },
        { ApiResponsiveImage.ContentType, typeof(ApiResponsiveImage) },
        { ApiMainNavigationLink.ContentType, typeof(ApiMainNavigationLink) },
        { ApiLinkWithSublinks.ContentType, typeof(ApiLinkWithSublinks) },
        { ApiHeadingWithLinks.ContentType, typeof(ApiHeadingWithLinks) },
        { ApiHeadingWithSocialLinks.ContentType, typeof(ApiHeadingWithSocialLinks) },
        // Components
        { ApiBanner.ContentType, typeof(ApiBanner) },
        { ApiCarousel.ContentType, typeof(ApiCarousel) },
        { ApiFullWidthImage.ContentType, typeof(ApiFullWidthImage) },
        { ApiGallery.ContentType, typeof(ApiGallery) },
        { ApiGallerySettings.ContentType, typeof(ApiGallerySettings) },
        { ApiRichTextSection.ContentType, typeof(ApiRichTextSection) },
        { ApiSectionHeader.ContentType, typeof(ApiSectionHeader) },
        { ApiSpotlight.ContentType, typeof(ApiSpotlight) },
        { ApiBlogListing.ContentType, typeof(ApiBlogListing) },
        { ApiBlogListingSettings.ContentType, typeof(ApiBlogListingSettings) },
        // Layouts
        { ApiOneColumn.ContentType, typeof(ApiOneColumn) },
    };

    public override IApiElement? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDocument = JsonDocument.ParseValue(ref reader);
        var hasContentType = jsonDocument.RootElement.TryGetProperty("contentType", out var contentType);

        if (!hasContentType)
        {
            throw new JsonException("No content type found.");
        }

        var discriminator = contentType.GetString();

        if (string.IsNullOrWhiteSpace(discriminator))
        {
            throw new JsonException("No discriminator");
        }

        var hasType = s_elementMap.TryGetValue(discriminator, out var apiContentType);

        if (!hasType)
        {
            throw new JsonException($"No content type found for '{discriminator}'.");
        }

        return JsonSerializer.Deserialize(jsonDocument.RootElement.GetRawText(), apiContentType!, options) as IApiElement;
    }

    public override void Write(Utf8JsonWriter writer, IApiElement value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
