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

    public async Task<ResponsiveImage?> Map(ApiResponsiveImage apiModel)
    {
        var mainImage = apiModel.Properties.MainImage?.FirstOrDefault();
        var mobileImage = apiModel.Properties.MobileImage?.FirstOrDefault();

        return new()
        {
            MainImage = mainImage is not null ? await _imageMapper.Map(mainImage) : null,
            MobileImage = mobileImage is not null ? await _imageMapper.Map(mobileImage) : null,
            AltText = apiModel.Properties.AltText
        };
    }
}
