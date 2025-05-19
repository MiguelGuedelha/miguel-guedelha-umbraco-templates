using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Layouts.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Shared;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Pages;

internal sealed class HomeMapper : IPageMapper
{
    private readonly IEnumerable<ILayoutMapper> _layoutMappers;
    private readonly ISeoMapper _seoMapper;
    private readonly SiteResolutionContext _siteResolutionContext;
    private readonly SiteResolutionService _siteResolutionService;

    public HomeMapper(IEnumerable<ILayoutMapper> layoutMappers, ISeoMapper seoMapper,
        SiteResolutionContext siteResolutionContext, SiteResolutionService siteResolutionService)
    {
        _layoutMappers = layoutMappers;
        _seoMapper = seoMapper;
        _siteResolutionContext = siteResolutionContext;
        _siteResolutionService = siteResolutionService;
    }

    public bool CanMap(string type) => type == DeliveryApiConstants.ContentTypes.ApiHome;

    public async Task<IPage?> Map(IApiContent model)
    {
        if (model is not ApiHome apiModel)
        {
            return null;
        }

        var contentMappingTasks = apiModel.Properties.MainContent.Items
            .Select(x => _layoutMappers.First(mapper => mapper.CanMap(x.Content.ContentType)).Map(x))
            .ToArray();

        await Task.WhenAll(contentMappingTasks);

        var site = _siteResolutionContext.Site;

        var alternateSites = await _siteResolutionService.GetAlternateSites(site);

        return new Home
        {
            Content = new()
            {
                Id = model.Id,
                ContentType = model.ContentType,
                MainContent = contentMappingTasks.Select(x => x.Result).OfType<ILayout>().ToArray(),
                AdditionalProperties = new()
            },
            Context = new()
            {
                Seo = await _seoMapper.Map(apiModel.Properties),
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
            }
        };
    }
}
