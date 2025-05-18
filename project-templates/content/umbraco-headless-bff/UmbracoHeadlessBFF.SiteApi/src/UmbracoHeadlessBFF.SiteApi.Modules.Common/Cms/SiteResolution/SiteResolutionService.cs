using Microsoft.AspNetCore.Http;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.SiteResolution.Clients;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.SiteResolution.Contracts;
using UmbracoHeadlessBFF.SharedModules.Common.Correlation;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;

public sealed class SiteResolutionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISiteResolutionApi _siteResolutionApi;
    private readonly SiteResolutionContext _siteResolutionContext;

    public SiteResolutionService(IHttpContextAccessor httpContextAccessor, ISiteResolutionApi siteResolutionApi,
        SiteResolutionContext siteResolutionContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _siteResolutionApi = siteResolutionApi;
        _siteResolutionContext = siteResolutionContext;
    }

    public async Task<(string SiteId, SiteDefinition SiteDefinition)?> ResolveSite()
    {
        var context = _httpContextAccessor.HttpContext;

        if (context is null)
        {
            return null;
        }

        var hasSiteId = context.Request.Headers.TryGetValue(CorrelationConstants.Headers.SiteId, out var siteId);
        var hasSitePath = context.Request.Headers.TryGetValue(CorrelationConstants.Headers.SitePath, out var sitePath);
        var hasSiteHost = context.Request.Headers.TryGetValue(CorrelationConstants.Headers.SiteHost, out var siteHost);

        var isPreview = _siteResolutionContext.IsPreview;

        var sitesResponse = await _siteResolutionApi.GetSites(isPreview);

        if (!sitesResponse.IsSuccessStatusCode || sitesResponse.Content is null)
        {
            return null;
        }

        var sites = sitesResponse.Content;

        if (hasSiteId)
        {
            var parsedId = siteId.ToString();
            var matchingSiteId = sites.TryGetValue(parsedId, out var site);
            if (matchingSiteId && site is not null)
            {
                return (parsedId, site);
            }
        }

        if (!hasSiteHost || !hasSitePath)
        {
            return null;
        }

        var path = sitePath.ToString();

        var sitesByLongestPath = sites
            .SelectMany(x => x.Value.Domains.Select(y => new { x.Key, SiteDefinitionDomain = y }))
            .OrderByDescending(x => x.SiteDefinitionDomain.Domain);

        var foundSite = sitesByLongestPath.FirstOrDefault(x =>
            x.SiteDefinitionDomain.Domain.Equals(siteHost, StringComparison.OrdinalIgnoreCase)
            && path.StartsWith(x.SiteDefinitionDomain.Path, StringComparison.OrdinalIgnoreCase));

        if (foundSite is not null)
        {
            return (foundSite.Key, sites[foundSite.Key]);
        }

        return null;
    }
}
