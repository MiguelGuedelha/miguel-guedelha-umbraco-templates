using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Sitemap;

public static class SitemapConfiguration
{
    public static void MapSitemapEndpoints(this RouteGroupBuilder apiVersionGroup)
    {
        apiVersionGroup
            .MapGroup("/sitemap")
            .WithTags("Sitemap")
            .MapGetSitemap();
    }
}
