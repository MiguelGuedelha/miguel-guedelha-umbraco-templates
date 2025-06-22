using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Refit;
using UmbracoHeadlessBFF.SharedModules.Cms.Sitemap;
using UmbracoHeadlessBFF.SharedModules.Common.Caching;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Endpoints;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;
using ZiggyCreatures.Caching.Fusion;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Endpoints;

internal static class GetSitemapEndpoint
{
    private const string SitemapSegment = "sitemap.xml";

    public static RouteGroupBuilder MapGetSitemap(this RouteGroupBuilder builder)
    {
        builder
            .MapGet("/sitemap", Handle)
            .MapToApiVersion(EndpointConstants.Versions.V1);

        return builder;
    }

    private static async Task<Results<Ok<SitemapData>, NotFound>> Handle(
        ISitemapsApi sitemapsApi,
        SiteResolutionContext siteResolutionContext,
        IFusionCacheProvider fusionCacheProvider,
        IOptionsSnapshot<DefaultCachingOptions> defaultCachingOptions)
    {
        var path = siteResolutionContext.Path;
        var domain = siteResolutionContext.Domain;

        var domainEntry = siteResolutionContext.Site.Domains
            .First(x => x.Domain.Equals(domain, StringComparison.OrdinalIgnoreCase));

        var sitemapPath = path.Replace(domainEntry.Path, string.Empty);

        if (!sitemapPath.Equals(SitemapSegment, StringComparison.OrdinalIgnoreCase))
        {
            return TypedResults.NotFound();
        }

        var homepage = siteResolutionContext.Site.HomepageId;
        var culture = siteResolutionContext.Site.CultureInfo;
        var isPreview = siteResolutionContext.IsPreview;

        if (isPreview)
        {
            var response = await GetSitemapFactory();

            return response switch
            {
                { Content: { Items.Count: > 0 } content } => TypedResults.Ok(content),
                _ => TypedResults.NotFound(),
            };
        }

        var fusionCache = fusionCacheProvider.GetCache(CachingConstants.SiteApiCacheName);

        var data = await fusionCache.GetOrSetAsync<SitemapData?>(
            $"sitemap:home-id:{homepage}:culture:{culture}",
            async (ctx, ct) =>
            {
                var response = await GetSitemapFactory(ct);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    ctx.Options.SetDuration(TimeSpan.FromSeconds(defaultCachingOptions.Value.NullDuration));
                }

                return response.Content;
            },
            tags:
            [
                CacheTagConstants.Sitemaps,
                siteResolutionContext.Site.SiteSettingsId.ToString()
            ]);

        return data switch
        {
            { Items.Count: > 0 } => TypedResults.Ok(data),
            _ => TypedResults.NotFound(),
        };

        async Task<IApiResponse<SitemapData>> GetSitemapFactory(CancellationToken cancellationToken = default)
        {
            var response = await sitemapsApi.GetSitemap(homepage, culture, isPreview, cancellationToken);

            if (response is { IsSuccessful: false, StatusCode: not HttpStatusCode.NotFound })
            {
                throw new SiteApiException((int)response.StatusCode, response.ReasonPhrase);
            }

            return response;
        }
    }
}
