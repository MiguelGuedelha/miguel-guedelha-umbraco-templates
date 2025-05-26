using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Compositions;

public interface IApiRedirectSettingsProperties
{
    public IReadOnlyCollection<ApiLink>? RedirectLink { get; init; }
    public RedirectFallbackDirection? RedirectDirection { get; init; }
}
