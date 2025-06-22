using System.Net;
using Microsoft.Extensions.Options;
using Refit;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages;
using UmbracoHeadlessBFF.SharedModules.Cms.Links;
using UmbracoHeadlessBFF.SharedModules.Cms.SiteResolution;
using UmbracoHeadlessBFF.SharedModules.Common.Caching;
using UmbracoHeadlessBFF.SharedModules.Common.Strings;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;
using ZiggyCreatures.Caching.Fusion;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages;

internal interface IPagesService
{
    Task<IApiContent?> GetPage(Guid id, bool? isPreview = null, SiteDefinition? site = null);
    Task<IApiContent?> GetPage(string path);
    Task<PagedApiContent?> GetPages(int skip = 0, int take = 10, ContentFetchType? fetch = null,
        IReadOnlyList<ContentFilterType>? filter = null, ContentSortType? sort = null, string? startItem = null);
}

internal sealed class PagesService : IPagesService
{
    private readonly IUmbracoDeliveryApi _umbracoDeliveryApi;
    private readonly SiteResolutionContext _siteResolutionContext;
    private readonly ILinksApi _linksApi;
    private readonly IFusionCache _fusionCache;
    private readonly IOptionsSnapshot<DefaultCachingOptions> _defaultCachingOptions;

    private static readonly string s_levelOneExpandFieldsLevel = new FieldsExpandProperties(1).ToString();
    private static readonly string s_defaultExpandFieldsLevel = new FieldsExpandProperties(5).ToString();

    public PagesService(
        IUmbracoDeliveryApi umbracoDeliveryApi,
        SiteResolutionContext siteResolutionContext,
        ILinksApi linksApi, IFusionCacheProvider fusionCacheProvider,
        IOptionsSnapshot<DefaultCachingOptions> defaultCachingOptions)
    {
        _umbracoDeliveryApi = umbracoDeliveryApi;
        _siteResolutionContext = siteResolutionContext;
        _linksApi = linksApi;
        _fusionCache = fusionCacheProvider.GetCache(CachingConstants.SiteApiCacheName);
        _defaultCachingOptions = defaultCachingOptions;
    }

    public async Task<IApiContent?> GetPage(Guid id, bool? isPreview = null, SiteDefinition? site = null)
    {
        var requestSite = site ?? _siteResolutionContext.Site;

        var requestPreview = isPreview ?? _siteResolutionContext.IsPreview;

        if (requestPreview)
        {
            var response = await GetPageByIdFactory(true, requestSite);
            return response.Content;
        }

        return await _fusionCache.GetOrSetAsync<IApiContent?>(
            $"page:home-id:{requestSite.HomepageId}:culture:{requestSite.CultureInfo}:page-id:{id}",
            async (ctx, ct) =>
            {
                var response = await GetPageByIdFactory(false, requestSite, ct);

                if (response.Content is null)
                {
                    ctx.Options.SetDuration(TimeSpan.FromSeconds(_defaultCachingOptions.Value.NullDuration));
                }

                return response.Content;
            },
            tags:
            [
                CacheTagConstants.Pages,
                id.ToString(),
                _siteResolutionContext.Site.SiteSettingsId.ToString()
            ]);

        async Task<IApiResponse<IApiContent>> GetPageByIdFactory(bool factoryPreview, SiteDefinition factorySite, CancellationToken cancellationToken = default)
        {
            var response = await _umbracoDeliveryApi.GetItemById(
                id: id,
                expand: s_defaultExpandFieldsLevel,
                acceptLanguage: factorySite.CultureInfo,
                preview: factoryPreview,
                startItem: factorySite.RootId.ToString(),
                cancellationToken: cancellationToken);

            if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NotFound)
            {
                return response;
            }

            throw new SiteApiException((int)response.StatusCode, response.ReasonPhrase);
        }
    }

    public async Task<IApiContent?> GetPage(string path)
    {
        var sanitizedPath = path.SanitisePathSlashes();

        var site = _siteResolutionContext.Site;

        var matchingDomain = site.Domains.FirstOrDefault(x => sanitizedPath.StartsWith(x.Path) && _siteResolutionContext.Domain == x.Domain);

        if (matchingDomain is null)
        {
            return null;
        }

        if (!_siteResolutionContext.IsPreview)
        {
            var redirectPath = sanitizedPath.Replace(matchingDomain.Path, "/");

            var redirect = await _fusionCache.GetOrSetAsync<RedirectLink?>(
                $"redirects:home-id:{site.HomepageId}:culture:{site.CultureInfo}:path:{sanitizedPath}",
                async (ctx, ct) =>
                {
                    var redirectResponse = await _linksApi.GetRedirect(redirectPath,
                        site.HomepageId,
                        site.CultureInfo,
                        ct);

                    if (!redirectResponse.IsSuccessStatusCode && redirectResponse.StatusCode != HttpStatusCode.NotFound)
                    {
                        throw new SiteApiException((int)redirectResponse.StatusCode, redirectResponse.ReasonPhrase);
                    }

                    return redirectResponse.Content;
                },
                tags:
                [
                    CacheTagConstants.Redirects,
                    _siteResolutionContext.Site.SiteSettingsId.ToString()
                ]);

            if (redirect is not null)
            {
                throw new SiteApiRedirectException(redirect.StatusCode, redirect.Location);
            }
        }

        var deliveryApiPath = site.RootId == site.HomepageId
            ? sanitizedPath.Replace(matchingDomain.Path, "/").SanitisePathSlashes()
            : sanitizedPath.Replace(matchingDomain.Path, site.BasePath).SanitisePathSlashes();

        if (_siteResolutionContext.IsPreview)
        {
            var response = await GetPageByPathFactory();
            return response.Content;
        }

        return await _fusionCache.GetOrSetAsync<IApiContent?>(
            $"page:home-id:{site.HomepageId}:culture:{site.CultureInfo}:path:{sanitizedPath}",
            async (ctx, ct) =>
            {
                var response = await GetPageByPathFactory(ct);

                if (response.Content is null)
                {
                    ctx.Options.SetDuration(TimeSpan.FromSeconds(_defaultCachingOptions.Value.NullDuration));
                }

                if (response.Content is { } page)
                {
                    ctx.Tags = ctx.Tags?.Append(page.Id.ToString()).ToArray();
                }

                return response.Content;
            },
            tags: [CacheTagConstants.Pages]);

        async Task<IApiResponse<IApiContent>> GetPageByPathFactory(CancellationToken cancellationToken = default)
        {
            var response = await _umbracoDeliveryApi.GetItemByPath(
                path: deliveryApiPath,
                expand: s_defaultExpandFieldsLevel,
                acceptLanguage: site.CultureInfo,
                preview: _siteResolutionContext.IsPreview,
                startItem: site.RootId.ToString(),
                cancellationToken: cancellationToken);

            if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NotFound)
            {
                return response;
            }

            throw new SiteApiException((int)response.StatusCode, response.ReasonPhrase);
        }
    }

    public async Task<PagedApiContent?> GetPages(
        int skip = 0,
        int take = 10,
        ContentFetchType? fetch = null,
        IReadOnlyList<ContentFilterType>? filter = null,
        ContentSortType? sort = null,
        string? startItem = null)
    {
        var site = _siteResolutionContext.Site;

        if (_siteResolutionContext.IsPreview)
        {
            var response = await GetPagesFactory();
            return response.Content;
        }

        var startItemSegment = startItem ?? "none";
        var fetchSegment = fetch is null ? "no-fetch" : fetch.ToString().Replace(':', '-');
        var filterSegment = filter is null or [] ? "no-filter" : string.Join("-", filter.Select(x => x.ToString()).Order());
        var sortSegment = sort is null ? "no-sort" : sort.ToString().Replace(':', '-');
        var sizeSegment = $"{skip}-{take}";

        var tags = new List<string> { CacheTagConstants.Pages };

        if (fetch is not null)
        {
            var isGuid = Guid.TryParse(fetch.IdOrPath, out var relatedId);

            var id = isGuid switch
            {
                true => relatedId,
                false => (await GetPage(fetch.IdOrPath))?.Id
            };

            if (id is not null)
            {
                tags.Add(id.Value.ToString());
            }
        }

        var data = await _fusionCache.GetOrSetAsync<PagedApiContent?>(
            $"pages:start-item:{startItemSegment}:culture:{site.CultureInfo}:params:{sizeSegment}_{fetchSegment}_{filterSegment}_{sortSegment}",
            async (ctx, ct) =>
            {
                var response = await GetPagesFactory(ct);

                if (response.Content is null)
                {
                    ctx.Options.SetDuration(TimeSpan.FromSeconds(_defaultCachingOptions.Value.NullDuration));
                }

                return response.Content;
            },
            tags: tags);

        return data;

        async Task<IApiResponse<PagedApiContent>> GetPagesFactory(CancellationToken cancellationToken = default)
        {
            var response = await _umbracoDeliveryApi.GetContent(
                fetch?.ToString(),
                filter?.Select(x => x.ToString()),
                sort?.ToString(),
                skip,
                take,
                s_levelOneExpandFieldsLevel,
                acceptLanguage: site.CultureInfo,
                preview: _siteResolutionContext.IsPreview,
                startItem: startItem,
                cancellationToken: cancellationToken);

            if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NotFound)
            {
                return response;
            }

            throw new SiteApiException((int)response.StatusCode, response.ReasonPhrase);
        }
    }
}
