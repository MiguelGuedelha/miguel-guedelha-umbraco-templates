using Refit;
using UmbracoHeadlessBFF.SharedModules.Common.Content.Sitemaps.Models;

namespace UmbracoHeadlessBFF.SharedModules.Common.Content.Sitemaps.Clients;

public interface ISitemapsApi
{
    [Get("/sitemap")]
    Task<IApiResponse<Sitemap>> GetSitemap(Guid siteId, string culture);
}
