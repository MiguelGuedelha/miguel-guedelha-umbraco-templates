using Refit;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.Links.Models;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.Links.Clients;

public interface ILinksApi
{
    [Get("/{id}")]
    Task<ApiResponse<Link>> GetLink(Guid id, string culture, bool preview);

    [Get("/redirects/{path}")]
    Task<ApiResponse<RedirectLink>> GetRedirect(string path, Guid siteId, string culture);
}
