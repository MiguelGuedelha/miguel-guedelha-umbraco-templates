using System.Net;
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

    private static readonly HttpStatusCode[] s_redirectCodes = [
        HttpStatusCode.Found,
        HttpStatusCode.Moved,
        HttpStatusCode.TemporaryRedirect,
        HttpStatusCode.PermanentRedirect
    ];

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
            expand: DeliveryApiRequestHelper.GeneratePropertiesAllValue(5),
            acceptLanguage: site.CultureInfo,
            preview: _siteResolutionContext.IsPreview,
            startItem: site.RootId.ToString());

        if (response.IsSuccessStatusCode)
        {
            return response.Content;
        }

        if (!s_redirectCodes.Contains(response.StatusCode))
        {
            throw new SiteApiException((int)response.StatusCode, response.ReasonPhrase);
        }

        throw new RedirectApiException((int)response.StatusCode, response.Headers.Location!.ToString());

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
            expand: DeliveryApiRequestHelper.GeneratePropertiesAllValue(5),
            acceptLanguage: site.CultureInfo,
            preview: _siteResolutionContext.IsPreview,
            startItem: site.RootId.ToString());

        if (!response.IsSuccessStatusCode)
        {
            throw new SiteApiException((int)response.StatusCode, response.ReasonPhrase);
        }

        return response.Content;
    }
}
