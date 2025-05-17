using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Clients;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Helpers;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.Strings;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content;

internal sealed class ContentService
{
    private readonly IUmbracoDeliveryApi _umbracoDeliveryApi;
    private readonly SiteResolutionContext _siteResolutionContext;

    public ContentService(IUmbracoDeliveryApi umbracoDeliveryApi, SiteResolutionContext siteResolutionContext)
    {
        _umbracoDeliveryApi = umbracoDeliveryApi;
        _siteResolutionContext = siteResolutionContext;
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

        if (!response.IsSuccessStatusCode)
        {
            throw new SiteApiException((int)response.StatusCode, response.ReasonPhrase);
        }

        return response.Content;
    }

    public async Task<IApiContent?> GetContentByPath(string path)
    {
        var sanitizedPath = path.SanitiseLeadingSlashes();

        var site = _siteResolutionContext.Site;

        var response = await _umbracoDeliveryApi.GetItemByPath(
            path: sanitizedPath,
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
