using Refit;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.SiteResolution.Contracts;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.SiteResolution.Clients;

public interface ISiteResolutionApi
{
    [Get("/")]
    Task<ApiResponse<Dictionary<Guid, SiteDefinition>>> GetSites(bool preview);
}
