using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.Links;
using UmbracoHeadlessBFF.SharedModules.Common.Strings;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content;

internal sealed class ContentService
{
    private readonly IUmbracoDeliveryApi _umbracoDeliveryApi;
    private readonly SiteResolutionContext _siteResolutionContext;
    private readonly ILinksApi _linksApi;

    private static readonly string s_levelOneExpandFieldsLevel = new FieldsExpandProperties(1).ToString();
    private static readonly string s_defaultExpandFieldsLevel = new FieldsExpandProperties(5).ToString();

    public ContentService(IUmbracoDeliveryApi umbracoDeliveryApi, SiteResolutionContext siteResolutionContext, ILinksApi linksApi)
    {
        _umbracoDeliveryApi = umbracoDeliveryApi;
        _siteResolutionContext = siteResolutionContext;
        _linksApi = linksApi;
    }

    public async Task<IApiContent?> GetContentById(Guid id)
    {
        var site = _siteResolutionContext.Site;

        var response = await _umbracoDeliveryApi.GetItemById(
            id: id,
            expand: s_defaultExpandFieldsLevel,
            acceptLanguage: site.CultureInfo,
            preview: _siteResolutionContext.IsPreview,
            startItem: site.RootId.ToString());

        if (!response.IsSuccessStatusCode)
        {
            throw new SiteApiException((int)response.StatusCode, response.ReasonPhrase);
        }

        return response.Content;
    }

    public async Task<IApiContent?> GetContentByPath(string path)
    {
        var sanitizedPath = path.SanitisePathSlashes();

        var site = _siteResolutionContext.Site;

        var matchingDomain = site.Domains.FirstOrDefault(x => path.StartsWith(x.Path) && _siteResolutionContext.Domain == x.Domain)
                             ?? site.Domains.First();

        if (matchingDomain is null)
        {
            throw new SiteApiException(404, "No matching domain found");
        }

        if (!_siteResolutionContext.IsPreview)
        {
            var redirectPath = sanitizedPath.Replace(matchingDomain.Path, "/");

            var redirectResponse = await _linksApi.GetRedirect(redirectPath, _siteResolutionContext.Site.HomepageId, _siteResolutionContext.Site.CultureInfo);

            if (redirectResponse.IsSuccessStatusCode)
            {
                throw new RedirectApiException(redirectResponse.Content!.StatusCode, redirectResponse.Content!.Location);
            }
        }

        var deliveryApiPath = site.RootId == site.HomepageId
            ? sanitizedPath.Replace(matchingDomain.Path, "/").SanitisePathSlashes()
            : sanitizedPath.Replace(matchingDomain.Path, site.BasePath).SanitisePathSlashes();

        var response = await _umbracoDeliveryApi.GetItemByPath(
            path: deliveryApiPath,
            expand: s_defaultExpandFieldsLevel,
            acceptLanguage: site.CultureInfo,
            preview: _siteResolutionContext.IsPreview,
            startItem: site.RootId.ToString());

        if (!response.IsSuccessStatusCode)
        {
            throw new SiteApiException((int)response.StatusCode, response.ReasonPhrase);
        }

        return response.Content;
    }

    public async Task<PagedApiContent?> GetPagedContent(
        int skip = 0,
        int take = 10,
        ContentFetchType? fetch = null,
        IReadOnlyList<ContentFilterType>? filter = null,
        ContentSortType? sort = null,
        string? startItem = null)
    {
        var site = _siteResolutionContext.Site;

        var response = await _umbracoDeliveryApi.GetContent(
            fetch?.ToString(),
            filter?.Select(x => x.ToString()),
            sort?.ToString(),
            skip,
            take,
            s_levelOneExpandFieldsLevel,
            acceptLanguage: site.CultureInfo,
            preview: _siteResolutionContext.IsPreview,
            startItem: startItem);

        if (!response.IsSuccessStatusCode)
        {
            throw new SiteApiException((int)response.StatusCode, response.ReasonPhrase);
        }

        return response.Content;
    }
}
