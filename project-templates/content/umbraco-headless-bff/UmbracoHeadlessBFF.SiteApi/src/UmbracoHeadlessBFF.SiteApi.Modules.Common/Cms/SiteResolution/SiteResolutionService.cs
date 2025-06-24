using Microsoft.AspNetCore.Http;
using UmbracoHeadlessBFF.SharedModules.Cms.SiteResolution;
using UmbracoHeadlessBFF.SharedModules.Common.Correlation;
using UmbracoHeadlessBFF.SharedModules.Common.Strings;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;
using ZiggyCreatures.Caching.Fusion;
using CachingConstants = UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching.CachingConstants;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;

public sealed class SiteResolutionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISiteResolutionApi _siteResolutionApi;
    private readonly SiteResolutionContext _siteResolutionContext;
    private readonly IFusionCache _fusionCache;

    public SiteResolutionService(
        IHttpContextAccessor httpContextAccessor,
        ISiteResolutionApi siteResolutionApi,
        SiteResolutionContext siteResolutionContext,
        IFusionCacheProvider fusionCacheProvider)
    {
        _httpContextAccessor = httpContextAccessor;
        _siteResolutionApi = siteResolutionApi;
        _siteResolutionContext = siteResolutionContext;
        _fusionCache = fusionCacheProvider.GetCache(CachingConstants.SiteApiCacheName);
    }

    public async Task<(string SiteId, SiteDefinition SiteDefinition)?> ResolveSite()
    {
        var context = _httpContextAccessor.HttpContext;

        if (context is null)
        {
            throw new SiteApiException("No http context");
        }

        var hasSiteId = context.Request.Headers.TryGetValue(CorrelationConstants.Headers.SiteId, out var siteId);
        var hasSitePath = context.Request.Headers.TryGetValue(CorrelationConstants.Headers.SitePath, out var sitePath);
        var hasSiteHost = context.Request.Headers.TryGetValue(CorrelationConstants.Headers.SiteHost, out var siteHost);

        var sites = await GetSitesInternal();

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

        var path = sitePath.ToString().SanitisePathSlashes();

        var sitesByLongestPath = sites
            .SelectMany(x => x.Value.Domains.Select(y => (x.Key, SiteDefinitionDomain: y)))
            .OrderByDescending(x => x.SiteDefinitionDomain.Path);

        var foundSite = sitesByLongestPath.FirstOrDefault(x =>
            x.SiteDefinitionDomain.Domain.Equals(siteHost, StringComparison.OrdinalIgnoreCase)
            && path.StartsWith(x.SiteDefinitionDomain.Path, StringComparison.OrdinalIgnoreCase));

        if (foundSite is { Key: not null, SiteDefinitionDomain: not null })
        {
            return (foundSite.Key, sites[foundSite.Key]);
        }

        throw new SiteApiException(StatusCodes.Status404NotFound, "No site found");
    }

    public async Task<IReadOnlyCollection<SiteDefinition>> GetAlternateSites(SiteDefinition site)
    {
        var sites = await GetSitesInternal();

        var alternateSites = sites
            .Where(x => x.Value.HomepageId == site.HomepageId && x.Value.CultureInfo != site.CultureInfo)
            .Select(x => x.Value)
            .ToArray();

        return alternateSites;
    }

    private async Task<Dictionary<string, SiteDefinition>> GetSitesInternal()
    {
        if (_siteResolutionContext.IsPreview)
        {
            return await GetSitesFactory();
        }

        return await _fusionCache.GetOrSetAsync<Dictionary<string, SiteDefinition>>(
            "sites",
            async (_, ct) => await GetSitesFactory(ct),
            tags: [CachingTagConstants.Sites]);

        async Task<Dictionary<string, SiteDefinition>> GetSitesFactory(CancellationToken cancellationToken = default)
        {
            var response = await _siteResolutionApi.GetSites(_siteResolutionContext.IsPreview, cancellationToken);

            if (response is { IsSuccessful: true, Content: not null })
            {
                return response.Content;
            }

            throw new SiteApiException((int)response.StatusCode, "Couldn't get sites", response.Error);
        }
    }
}
