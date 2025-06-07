using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages.Compositions;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages.Extensions;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.Layouts;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Layouts;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Pages;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.Pages;

internal sealed class BasePageMapper
{
    private readonly ISeoMapper _seoMapper;
    private readonly ISiteSettingsMapper _siteSettingsMapper;
    private readonly SiteResolutionContext _siteResolutionContext;
    private readonly SiteResolutionService _siteResolutionService;
    private readonly IEnumerable<ILayoutMapper> _layoutMappers;
    private readonly IPageService _pageService;

    public BasePageMapper(ISeoMapper seoMapper, ISiteSettingsMapper siteSettingsMapper,
        SiteResolutionContext siteResolutionContext, SiteResolutionService siteResolutionService,
        IEnumerable<ILayoutMapper> layoutMappers, IPageService pageService)
    {
        _seoMapper = seoMapper;
        _siteResolutionContext = siteResolutionContext;
        _siteResolutionService = siteResolutionService;
        _layoutMappers = layoutMappers;
        _pageService = pageService;
        _siteSettingsMapper = siteSettingsMapper;
    }

    public async Task<PageContext> MapPageContext<T>(IApiContent<T> model)
    {
        var site = _siteResolutionContext.Site;

        var alternateSites = await _siteResolutionService.GetAlternateSites(site);

        var seo = model.Properties as IApiSeoSettingsProperties;

        var siteSettings = await _pageService.GetPage(site.SiteSettingsId) as ApiSiteSettings;

        return new()
        {
            Seo = seo is not null ? await _seoMapper.Map(seo) : null,
            SiteSettings = siteSettings is not null
                ? await _siteSettingsMapper.Map(siteSettings)
                : null,
            Breadcrumbs = await MapBreadcrumbs(model),
            SiteResolution = new()
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

    private async Task<Breadcrumbs?> MapBreadcrumbs<T>(IApiContent<T> model)
    {
        if (model.Properties is not IApiNavigationSettingsProperties { ShowBreadcrumbs: true } navigationSettings)
        {
            return null;
        }

        var fetch = new ContentFetchType
        {
            FetchType = ContentFetchType.Options.Ancestors,
            IdOrPath = model.Id.ToString(),
        };

        var sort = new ContentSortType
        {
            SortType = ContentSortType.Options.LevelAscending
        };

        var ancestors = await _pageService.GetPages(
            fetch: fetch,
            sort: sort,
            startItem: _siteResolutionContext.Site.RootId.ToString());

        var breadcrumbs = new List<BreadcrumbItem>();

        foreach (var ancestor in ancestors?.Items ?? [])
        {
            var hasProps = ancestor.TryGetProperties<IApiNavigationSettingsProperties>(out var props);

            if (!hasProps || props is not { ShowInBreadcrumbs: true })
            {
                continue;
            }

            breadcrumbs.Add(new()
            {
                Name = props.BreadcrumbNameOverride ?? ancestor.Name,
                Href = props.ShowBreadcrumbLink ? ancestor.Route.Path : null,
            });
        }

        breadcrumbs.Add(new()
        {
            Name = navigationSettings.BreadcrumbNameOverride ?? model.Name,
            Href = null,
        });

        return breadcrumbs.Count == 1
            ? null
            : new()
            {
                Items = breadcrumbs,
            };
    }
}
