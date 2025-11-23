using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.BuildingBlocks;

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

    public async Task<ResponsiveImage?> Map(ApiResponsiveImage? model)
    {
        if (model is null)
        {
            return null;
        }

        var mainImage = model.Properties.MainImage?.FirstOrDefault();
        var mobileImage = model.Properties.MobileImage?.FirstOrDefault();

        return new()
        {
            MainImage = await _imageMapper.Map(mainImage),
            MobileImage = await _imageMapper.Map(mobileImage),
            AltText = model.Properties.AltText
        };
    }
}
