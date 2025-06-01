using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using UmbracoHeadlessBFF.SharedModules.Content.Sitemap;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Endpoints;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Endpoints;

internal static class GetSitemapEndpoint
{
    private static readonly string s_sitemapSegment = "sitemap.xml";

    public static RouteGroupBuilder MapGetSitemap(this RouteGroupBuilder builder)
    {
        builder
            .MapGet("/sitemap", Handler)
            .MapToApiVersion(EndpointConstants.Versions.V1);

        return builder;
    }

    private static async Task<Results<Ok<SitemapData>, NotFound>> Handler(
        ISitemapsApi sitemapsApi,
        SiteResolutionContext siteResolutionContext)
    {
        var path = siteResolutionContext.Path;
        var domain = siteResolutionContext.Domain;

        var domainEntry = siteResolutionContext.Site.Domains
            .First(x => x.Domain.Equals(domain, StringComparison.OrdinalIgnoreCase));

        var sitemapPath = path.Replace(domainEntry.Path, string.Empty);

        if (!sitemapPath.Equals(s_sitemapSegment, StringComparison.OrdinalIgnoreCase))
        {
            return TypedResults.NotFound();
        }

        var response = await sitemapsApi.GetSitemap(siteResolutionContext.Site.HomepageId, siteResolutionContext.Site.CultureInfo, siteResolutionContext.IsPreview);

        return response switch
        {
            { Content: { Items.Count: > 0 } content } => TypedResults.Ok(content),
            _ => TypedResults.NotFound(),
        };
    }
}
