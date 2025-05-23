using Refit;

namespace UmbracoHeadlessBFF.SharedModules.Content.Sitemaps;

public interface ISitemapsApi
{
    [Get("/sitemap")]
    Task<IApiResponse<Sitemap>> GetSitemap(Guid siteId, string culture);
}
