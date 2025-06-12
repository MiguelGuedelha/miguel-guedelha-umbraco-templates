using System.Net;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Refit;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages;
using UmbracoHeadlessBFF.SharedModules.Cms.Robots;
using UmbracoHeadlessBFF.SharedModules.Common.Caching;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Endpoints;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Pages;
using ZiggyCreatures.Caching.Fusion;
using NotFound = Microsoft.AspNetCore.Http.HttpResults.NotFound;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Endpoints;

internal static class GetRobotsEndpoint
{
    private const string RobotsTxtSegment = "robots.txt";

    public static RouteGroupBuilder MapGetRobots(this RouteGroupBuilder builder)
    {
        builder
            .MapGet("/robots", Handle)
            .MapToApiVersion(EndpointConstants.Versions.V1);

        return builder;
    }

    private static async Task<Results<Ok<RobotsTxt>, NotFound>> Handle(
        IRobotsApi robotsApi,
        SiteResolutionContext siteResolutionContext,
        IFusionCache fusionCache,
        IOptionsSnapshot<DefaultCachingOptions> defaultCachingOptions,
        IPagesService pagesService,
        SiteResolutionService siteResolutionService)
    {
        var path = siteResolutionContext.Path;
        var domain = siteResolutionContext.Domain;

        var domainEntry = siteResolutionContext.Site.Domains
            .First(x => x.Domain.Equals(domain, StringComparison.OrdinalIgnoreCase));

        var robotsPath = path.Replace(domainEntry.Path, string.Empty);

        if (!robotsPath.Equals(RobotsTxtSegment, StringComparison.OrdinalIgnoreCase))
        {
            return TypedResults.NotFound();
        }

        var homepage = siteResolutionContext.Site.HomepageId;
        var culture = siteResolutionContext.Site.CultureInfo;
        var isPreview = siteResolutionContext.IsPreview;

        string? content;

        if (isPreview)
        {
            var response = await GetRobotsTxtFactory();

            content = response.Content?.RobotsContent;
        }
        else
        {
            var data = await fusionCache.GetOrSetAsync<RobotsData?>(
                $"robots:home-id:{homepage}:culture:{culture}",
                async (ctx, ct) =>
                {
                    var response = await GetRobotsTxtFactory(ct);

                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        ctx.Options.SetDuration(TimeSpan.FromSeconds(defaultCachingOptions.Value.NullDuration));
                    }

                    return response.Content;
                },
                tags: [CacheTagConstants.Robots]);

            content = data?.RobotsContent;
        }

        if (content is null)
        {
            return TypedResults.NotFound();
        }

        var siteSettings = await pagesService.GetPage(siteResolutionContext.Site.SiteSettingsId) as ApiSiteSettings;
        var siteDomain = siteResolutionContext.Site.Domains.First();
        var canonicalDomain = siteSettings?.Properties.CanonicalDomainOverride ?? $"{siteDomain.Scheme}://{siteDomain.Domain}{siteDomain.Path}";

        var sitemapUrls = new List<Uri> { new(new(canonicalDomain), "sitemap.xml") };

        var alternateSites = await siteResolutionService.GetAlternateSites(siteResolutionContext.Site);

        var alternateCanonicalTasks = alternateSites
            .Select(x => pagesService.GetPage(siteResolutionContext.Site.SiteSettingsId, isPreview, x))
            .ToArray();

        await Task.WhenAll(alternateCanonicalTasks);

        var alternateSitemapUrls = alternateCanonicalTasks
            .Select(x => x.Result as ApiSiteSettings)
            .Zip(alternateSites)
            .Select(x =>
            {
                var alternateDomain = x.Second.Domains.First();

                return x.First?.Properties.CanonicalDomainOverride ?? $"{alternateDomain.Scheme}://{alternateDomain.Domain}{alternateDomain.Path}";
            })
            .Select(x => new Uri(new(x), "sitemap.xml"));

        sitemapUrls.AddRange(alternateSitemapUrls);

        var robotsSitemaps = string.Join('\n', sitemapUrls.Select(x => $"Sitemap: {x.ToString()}"));

        return TypedResults.Ok(new RobotsTxt
        {
            Content = $"{content}\n{robotsSitemaps}"
        });

        async Task<IApiResponse<RobotsData>> GetRobotsTxtFactory(CancellationToken cancellationToken = default)
        {
            var response = await robotsApi.GetRobots(homepage, culture, isPreview, cancellationToken);

            if (response is { IsSuccessful: false, StatusCode: not HttpStatusCode.NotFound })
            {
                throw new SiteApiException((int)response.StatusCode, response.ReasonPhrase);
            }

            return response;
        }
    }
}
