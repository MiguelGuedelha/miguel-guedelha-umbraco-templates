using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Compositions;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Layouts;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Pages;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.Pages;

internal sealed class BasePageMapper
{
    private readonly ISeoMapper _seoMapper;
    private readonly SiteResolutionContext _siteResolutionContext;
    private readonly SiteResolutionService _siteResolutionService;
    private readonly IEnumerable<ILayoutMapper> _layoutMappers;
    private readonly ContentService _contentService;

    public BasePageMapper(ISeoMapper seoMapper, SiteResolutionContext siteResolutionContext,
        SiteResolutionService siteResolutionService, IEnumerable<ILayoutMapper> layoutMappers,
        ContentService contentService)
    {
        _seoMapper = seoMapper;
        _siteResolutionContext = siteResolutionContext;
        _siteResolutionService = siteResolutionService;
        _layoutMappers = layoutMappers;
        _contentService = contentService;
    }

    public async Task<PageContext> MapPageContext<T>(IApiContent<T> model)
    {
        var site = _siteResolutionContext.Site;

        var alternateSites = await _siteResolutionService.GetAlternateSites(site);

        var seo = model.Properties as IApiSeoSettingsProperties;



        var ancestors = _contentService.GetPagedContent();

        return new()
        {
            Seo = seo is not null ? await _seoMapper.Map(seo) : null,
            Site = new()
            {
                Locale = site.CultureInfo,
                Domain = site.Domains.First().Domain,
                Subpath = site.Domains.First().Path,
                AlternateLanguages = alternateSites.Select(x => new AlternateSites
                {
                    Locale = x.CultureInfo,
                    Domain = x.Domains.First().Domain,
                    Subpath = x.Domains.First().Path
                }).ToArray()
            }
        };
    }

    public async Task<IReadOnlyCollection<ILayout>> MapMainContent<T>(IApiContent<T> model)
        where T : IApiPageContent
    {
        var contentMappingTasks = model.Properties.MainContent.Items
            .Select(x => _layoutMappers.First(mapper => mapper.CanMap(x.Content.ContentType)).Map(x))
            .ToArray();

        await Task.WhenAll(contentMappingTasks);

        return contentMappingTasks.Select(x => x.Result).Where(x => x is not null).ToArray()!;
    }
}
