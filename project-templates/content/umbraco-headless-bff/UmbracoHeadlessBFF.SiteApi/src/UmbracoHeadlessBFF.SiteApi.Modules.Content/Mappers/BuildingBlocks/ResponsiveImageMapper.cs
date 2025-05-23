using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks.Media;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks.Media;

internal interface IResponsiveImageMapper : IMapper<ApiResponsiveImage, ResponsiveImage>
{
}

internal sealed class ResponsiveImageMapper : IResponsiveImageMapper
{
    private readonly IImageMapper _imageMapper;

    public ResponsiveImageMapper(IImageMapper imageMapper)
    {
        _imageMapper = imageMapper;
    }

    public async Task<ResponsiveImage?> Map(ApiResponsiveImage apiModel)
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

        var mainImage = apiModel.Properties.MainImage?.FirstOrDefault();
        var mobileImage = apiModel.Properties.MobileImage?.FirstOrDefault();

        return new()
        {
            MainImage = mainImage is not null ? await _imageMapper.Map(mainImage) : null,
            MobileImage = mobileImage is not null ? await _imageMapper.Map(mobileImage) : null,
            AltText = altText
        };
    }
}
