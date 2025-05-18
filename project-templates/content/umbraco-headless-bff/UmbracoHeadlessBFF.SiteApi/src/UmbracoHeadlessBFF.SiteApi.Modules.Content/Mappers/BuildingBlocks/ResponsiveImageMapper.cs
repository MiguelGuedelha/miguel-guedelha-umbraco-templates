using Microsoft.AspNetCore.Http.Extensions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Media;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;

internal sealed class ResponsiveImageMapper : IMapper<ApiResponsiveImage, ResponsiveImage>
{
    private static readonly HashSet<string> s_imageTypes = ["png", "webp", "jpg", "jpeg", "avif", "pjepg", "pjp"];
    private static readonly Uri s_basePlaceholderUrl = new("https://example.com");

    public Task<ResponsiveImage> Map(ApiResponsiveImage apiModel)
    {
        var altText = apiModel.Properties.AltText;

        if (string.IsNullOrWhiteSpace(altText) && apiModel.Properties.MainImage
                ?.FirstOrDefault()
                ?.Properties?.TryGetValue("altText", out var imageAlt) is true)
        {
            altText = imageAlt as string;
        }

        if (string.IsNullOrWhiteSpace(altText) && apiModel.Properties.MobileImage
                ?.FirstOrDefault()
                ?.Properties?.TryGetValue("altText", out imageAlt) is true)
        {
            altText = imageAlt as string;
        }

        return Task.FromResult(new ResponsiveImage
        {
            MainImage = MapImage(apiModel.Properties.MainImage?.FirstOrDefault()),
            MobileImage = MapImage(apiModel.Properties.MobileImage?.FirstOrDefault()),
            AltText = altText
        });
    }

    private static Image? MapImage(ApiMediaWithCrops? media)
    {
        if (media is null or { Extension: null })
        {
            return null;
        }

        if (!s_imageTypes.Contains(media.Extension))
        {
            return new Image { Href = media.Url, AltText = null };
        }

        var query = new QueryBuilder();

        if (media.FocalPoint is not null)
        {
            query.Add("rxy", $"{media.FocalPoint.Left},{media.FocalPoint.Top}");
        }

        query.Add("quality", "75");

        if (!media.Extension.Equals("webp", StringComparison.OrdinalIgnoreCase) &&
            !media.Extension.Equals("avif", StringComparison.OrdinalIgnoreCase))
        {
            query.Add("format", "webp");
        }

        var url = new UriBuilder(s_basePlaceholderUrl) { Path = media.Url, Query = query.ToString() };

        return new Image { Href = url.Uri.PathAndQuery, AltText = null };
    }
}
