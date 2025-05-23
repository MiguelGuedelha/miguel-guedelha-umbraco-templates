using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Layouts;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Pages;

internal abstract class BasePageMapper
{
    private readonly ISeoMapper _seoMapper;
    private readonly SiteResolutionContext _siteResolutionContext;
    private readonly SiteResolutionService _siteResolutionService;
    private readonly IEnumerable<ILayoutMapper> _layoutMappers;

    protected BasePageMapper(ISeoMapper seoMapper, SiteResolutionContext siteResolutionContext,
        SiteResolutionService siteResolutionService, IEnumerable<ILayoutMapper> layoutMappers)
    {
        _seoMapper = seoMapper;
        _siteResolutionContext = siteResolutionContext;
        _siteResolutionService = siteResolutionService;
        _layoutMappers = layoutMappers;
    }

    protected async Task<PageContext> MapPageContext<T>(T? pageProperties)
        where T : IApiSeoSettingsProperties
    {
        var site = _siteResolutionContext.Site;

        var alternateSites = await _siteResolutionService.GetAlternateSites(site);

        var seo = pageProperties as IApiSeoSettingsProperties;

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

    protected async Task<IReadOnlyCollection<ILayout>> MapMainContent<T>(ApiContent<T> model)
        where T : IApiPageContent
    {
        var contentMappingTasks = model.Properties.MainContent.Items
            .Select(x => _layoutMappers.First(mapper => mapper.CanMap(x.Content.ContentType)).Map(x))
            .ToArray();

        await Task.WhenAll(contentMappingTasks);

        return contentMappingTasks.Select(x => x.Result).Where(x => x is not null).ToArray()!;
    }
}
