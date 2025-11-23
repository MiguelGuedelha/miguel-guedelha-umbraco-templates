using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Configuration;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.BuildingBlocks;

internal interface IImageMapper : IMapper<ApiMediaWithCrops, Image>
{
}

internal sealed class ImageMapper : IImageMapper
{
    private static readonly HashSet<string> s_imageTypes = ["png", "webp", "jpg", "jpeg", "avif", "pjepg", "pjp"];

    private readonly ApplicationUrlOptions _applicationUrlOptions;

    public ImageMapper(IOptionsSnapshot<ApplicationUrlOptions> applicationUrlOptions)
    {
        _applicationUrlOptions = applicationUrlOptions.Value;
    }

    public Task<Image?> Map(ApiMediaWithCrops? model)
    {
        if (model is null or { Extension: null })
        {
            return Task.FromResult<Image?>(null);
        }

        object? altText = null;
        model.Properties?.TryGetValue("altText", out altText);

        if (!s_imageTypes.Contains(model.Extension))
        {
            return Task.FromResult<Image?>(new()
            {
                Src = new UriBuilder(_applicationUrlOptions.Media) { Path = model.Url.TrimStart('/') }.Uri.AbsoluteUri,
                AltText = altText as string
            });
        }

        var query = new QueryBuilder();

        if (model.FocalPoint is not null)
        {
            query.Add("rxy", $"{model.FocalPoint.Left},{model.FocalPoint.Top}");
        }

        if (!model.Extension.Equals("webp", StringComparison.OrdinalIgnoreCase) &&
            !model.Extension.Equals("avif", StringComparison.OrdinalIgnoreCase))
        {
            query.Add("format", "webp");
        }

        var url = new UriBuilder(_applicationUrlOptions.Media) { Path = model.Url, Query = query.ToString() };

        return Task.FromResult<Image?>(new()
        {
            Src = url.Uri.AbsoluteUri,
            AltText = altText as string
        });
    }
}
