using Refit;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.SiteResolution.Models;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.SiteResolution.Clients;

public interface ISiteResolutionApi
{
    [Get("/")]
    Task<ApiResponse<Dictionary<string, SiteDefinition>>> GetSites(bool preview);
}
