using Refit;

namespace UmbracoHeadlessBFF.SharedModules.Content.Sitemap;

public interface ISitemapsApi
{
    [Get("/sitemap")]
    Task<IApiResponse<SitemapData>> GetSitemap(Guid siteId, string culture, bool preview);
}
