using Refit;
using UmbracoHeadlessBFF.SharedModules.Content.Sitemaps.Models;

namespace UmbracoHeadlessBFF.SharedModules.Content.Sitemaps.Clients;

public interface ISitemapsApi
{
    [Get("/sitemap")]
    Task<IApiResponse<Sitemap>> GetSitemap(Guid siteId, string culture);
}
