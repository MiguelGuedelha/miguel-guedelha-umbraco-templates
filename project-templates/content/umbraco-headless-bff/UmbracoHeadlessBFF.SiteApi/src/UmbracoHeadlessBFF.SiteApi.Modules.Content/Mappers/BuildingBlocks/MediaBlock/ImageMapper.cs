using Microsoft.AspNetCore.Http.Extensions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Media;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks.MediaBlock;

internal sealed class ImageMapper : IMapper<ApiMediaWithCrops, Image>
{
    private static readonly HashSet<string> s_imageTypes = ["png", "webp", "jpg", "jpeg", "avif", "pjepg", "pjp"];
    private static readonly Uri s_basePlaceholderUrl = new("https://example.com");

    public Task<Image?> Map(ApiMediaWithCrops media)
    {
        if (media is null or { Extension: null })
        {
            return Task.FromResult<Image?>(null);
        }

        if (!s_imageTypes.Contains(media.Extension))
        {
            return Task.FromResult<Image?>(new() { Src = media.Url, AltText = null });
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

        return Task.FromResult<Image?>(new() { Src = url.Uri.PathAndQuery, AltText = null });
    }
}
