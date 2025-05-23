using Refit;

namespace UmbracoHeadlessBFF.SharedModules.Content.Sitemaps;

public interface ISitemapsApi
{
    [Get("/sitemap")]
    Task<IApiResponse<SitemapData>> GetSitemap(Guid siteId, string culture);
}
