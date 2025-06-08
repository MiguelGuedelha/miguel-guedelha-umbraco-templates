using System.Net;
using Microsoft.Extensions.Options;
using Refit;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages;
using UmbracoHeadlessBFF.SharedModules.Cms.Links;
using UmbracoHeadlessBFF.SharedModules.Common.Caching;
using UmbracoHeadlessBFF.SharedModules.Common.Strings;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;
using ZiggyCreatures.Caching.Fusion;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages;

internal interface IPageService
{
    Task<IApiContent?> GetPage(Guid id);
    Task<IApiContent?> GetPage(string path);
    Task<PagedApiContent?> GetPages(int skip = 0, int take = 10, ContentFetchType? fetch = null,
        IReadOnlyList<ContentFilterType>? filter = null, ContentSortType? sort = null, string? startItem = null);
}

internal sealed class PageService : IPageService
{
    private readonly IUmbracoDeliveryApi _umbracoDeliveryApi;
    private readonly SiteResolutionContext _siteResolutionContext;
    private readonly ILinksApi _linksApi;
    private readonly IFusionCache _fusionCache;
    private readonly IOptionsSnapshot<DefaultCachingOptions> _defaultCachingOptions;

    private static readonly string s_levelOneExpandFieldsLevel = new FieldsExpandProperties(1).ToString();
    private static readonly string s_defaultExpandFieldsLevel = new FieldsExpandProperties(5).ToString();

    public PageService(
        IUmbracoDeliveryApi umbracoDeliveryApi,
        SiteResolutionContext siteResolutionContext,
        ILinksApi linksApi, IFusionCache fusionCache,
        IOptionsSnapshot<DefaultCachingOptions> defaultCachingOptions)
    {
        _umbracoDeliveryApi = umbracoDeliveryApi;
        _siteResolutionContext = siteResolutionContext;
        _linksApi = linksApi;
        _fusionCache = fusionCache;
        _defaultCachingOptions = defaultCachingOptions;
    }

    public async Task<IApiContent?> GetPage(Guid id)
    {
        var site = _siteResolutionContext.Site;

        if (_siteResolutionContext.IsPreview)
        {
            var response = await GetPageByIdFactory();
            return response.Content;
        }

        return await _fusionCache.GetOrSetAsync<IApiContent?>(
            $"page:{site.HomepageId}:{site.CultureInfo}:{id}",
            async (ctx, ct) =>
            {
                var response = await GetPageByIdFactory(ct);

                if (response.Content is null)
                {
                    ctx.Options.SetDuration(TimeSpan.FromSeconds(_defaultCachingOptions.Value.NullDuration));
                }

                return response.Content;
            });

        async Task<IApiResponse<IApiContent>> GetPageByIdFactory(CancellationToken cancellationToken = default)
        {
            var response = await _umbracoDeliveryApi.GetItemById(
                id: id,
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

    public async Task<IApiContent?> GetPage(string path)
    {
        var sanitizedPath = path.SanitisePathSlashes();

        var site = _siteResolutionContext.Site;

        var matchingDomain = site.Domains.FirstOrDefault(x => path.StartsWith(x.Path) && _siteResolutionContext.Domain == x.Domain);

        if (matchingDomain is null)
        {
            return null;
        }

        if (!_siteResolutionContext.IsPreview)
        {
            var redirectPath = sanitizedPath.Replace(matchingDomain.Path, "/");

            var redirect = await _fusionCache.GetOrSetAsync<RedirectLink?>(
                $"redirects:{site.HomepageId}:{site.CultureInfo}:{sanitizedPath}",
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
                });

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
            $"page:{site.HomepageId}:{site.CultureInfo}:{sanitizedPath}",
            async (ctx, ct) =>
            {
                var response = await GetPageByPathFactory(ct);

                if (response.Content is null)
                {
                    ctx.Options.SetDuration(TimeSpan.FromSeconds(_defaultCachingOptions.Value.NullDuration));
                }

                return response.Content;
            });

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

        var startItemSegment = startItem ?? "no-start-item";
        var fetchSegment = fetch is null ? "no-fetch" : fetch.ToString().Replace(':', '-');
        var filterSegment = filter is null or [] ? "no-filter" : string.Join("-", filter.Select(x => x.ToString()).Order());
        var sortSegment = sort is null ? "no-sort" : sort.ToString().Replace(':', '-');
        var sizeSegment = $"{skip}-{take}";

        var data = await _fusionCache.GetOrSetAsync<PagedApiContent?>(
            $"pages:{startItemSegment}:{site.CultureInfo}:{sizeSegment}:{fetchSegment}:{filterSegment}:{sortSegment}",
            async (ctx, ct) =>
            {
                var response = await GetPagesFactory(ct);

                if (response.Content is null)
                {
                    ctx.Options.SetDuration(TimeSpan.FromSeconds(_defaultCachingOptions.Value.NullDuration));
                }

                return response.Content;
            });

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
