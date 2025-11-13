using Refit;

namespace UmbracoHeadlessBFF.SharedModules.Cms.Sitemap;

public interface ISitemapsApi
{
    [Get("/sitemap")]
    Task<IApiResponse<SitemapData>> GetSitemap(Guid siteId, string culture, bool preview, CancellationToken cancellationToken = default);
}
