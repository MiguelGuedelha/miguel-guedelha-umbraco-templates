using Refit;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.Links.Contracts;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.Links.Clients;

public interface ILinksApi
{
    [Get("/{id}")]
    Task<ApiResponse<Link>> GetLink(Guid id, string culture, bool preview);
}
