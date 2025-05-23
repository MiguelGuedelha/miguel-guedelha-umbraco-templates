using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Pages;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.Pages;

internal interface ISeoMapper : IMapper<IApiSeoSettingsProperties, Seo>
{
}

internal sealed class SeoMapper : ISeoMapper
{
    private readonly IImageMapper _imageMapper;

    public SeoMapper(IImageMapper imageMapper)
    {
        _imageMapper = imageMapper;
    }

    public async Task<Seo?> Map(IApiSeoSettingsProperties model)
    {
        var metaImage = model.MetaImage?.FirstOrDefault();
        var ogImage = model.OgImage?.FirstOrDefault();

        var mappedMetaImage = metaImage is not null ? await _imageMapper.Map(metaImage) : null;

        var mappedOgImage = (ogImage, metaImage) switch
        {
            (not null, _) => await _imageMapper.Map(ogImage),
            (_, not null) => mappedMetaImage,
            _ => null
        };

        return new()
        {
            MetaTitle = model.MetaTitle,
            MetaDescription = model.MetaDescription,
            MetaImage = mappedMetaImage?.Src,
            OgType = model.OgType,
            OgDescription = string.IsNullOrWhiteSpace(model.OgDescription) ? model.MetaDescription : model.OgDescription,
            OgImage = mappedOgImage?.Src,
            RobotsIndexOptions = string.Join(' ', model.RobotsIndexOptions ?? []),
            RobotsUnavailableAfter = model.RobotsUnavailableAfter
        };
    }
}
