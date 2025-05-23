using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Endpoints;
using UmbracoHeadlessBFF.SharedModules.Content.Sitemaps;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Sitemaps;

public static class GetSitemapEndpoint
{
    public static RouteGroupBuilder MapGetSitemap(this RouteGroupBuilder builder)
    {
        builder
            .MapGet("/sitemap", GetSitemapHandler)
            .MapToApiVersion(EndpointConstants.Versions.V1);

        return builder;
    }

    private static async Task<Results<Ok<SitemapData>, NotFound>> GetSitemapHandler(
        ISitemapsApi sitemapsApi,
        SiteResolutionContext siteResolutionContext)
    {
        var response = await sitemapsApi.GetSitemap(siteResolutionContext.Site.HomepageId, siteResolutionContext.Site.CultureInfo);

        return response switch
        {
            { Content: { Items.Count: > 0 } content } => TypedResults.Ok(content),
            _ => TypedResults.NotFound(),
        };
    }
}
