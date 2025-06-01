using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages.Compositions;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Pages;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.Pages;

internal interface ISeoMapper : IMapper<IApiSeoSettingsProperties, Seo>
{
}

internal sealed class SeoMapper : ISeoMapper
{
    private readonly IImageMapper _imageMapper;
    private readonly IPageService _pageService;
    private readonly SiteResolutionContext _siteResolutionContext;

    public SeoMapper(IImageMapper imageMapper, IPageService pageService, SiteResolutionContext siteResolutionContext)
    {
        _imageMapper = imageMapper;
        _pageService = pageService;
        _siteResolutionContext = siteResolutionContext;
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

        var siteSettings = await _pageService.GetPageById(_siteResolutionContext.Site.SiteSettingsId) as ApiSiteSettings;

        var titlePrefix = siteSettings?.Properties.PageTitlePrefix;
        var domain = _siteResolutionContext.Site.Domains.First();
        var canonical = siteSettings?.Properties.CanonicalDomainOverride
            ?? $"{domain.Scheme}://{domain.Domain}";

        return new()
        {
            CanonicalUrl = new Uri(new(canonical), _siteResolutionContext.Path).ToString(),
            MetaTitle = titlePrefix is null ? model.MetaTitle : $"{titlePrefix}{model.MetaTitle}",
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
