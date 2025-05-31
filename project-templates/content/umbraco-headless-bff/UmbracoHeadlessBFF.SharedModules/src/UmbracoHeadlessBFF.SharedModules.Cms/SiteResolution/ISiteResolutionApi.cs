using Refit;

namespace UmbracoHeadlessBFF.SharedModules.Cms.SiteResolution;

public interface ISiteResolutionApi
{
    [Get("/")]
    Task<ApiResponse<Dictionary<string, SiteDefinition>>> GetSites(bool preview);
}
