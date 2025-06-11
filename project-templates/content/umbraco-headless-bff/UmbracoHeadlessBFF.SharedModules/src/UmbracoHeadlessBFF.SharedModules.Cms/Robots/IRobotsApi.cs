using Refit;

namespace UmbracoHeadlessBFF.SharedModules.Cms.Robots;

public interface IRobotsApi
{
    [Get("/robots")]
    Task<IApiResponse<RobotsData>> GetRobots(Guid siteId, string culture, bool preview, CancellationToken cancellationToken = default);
}
