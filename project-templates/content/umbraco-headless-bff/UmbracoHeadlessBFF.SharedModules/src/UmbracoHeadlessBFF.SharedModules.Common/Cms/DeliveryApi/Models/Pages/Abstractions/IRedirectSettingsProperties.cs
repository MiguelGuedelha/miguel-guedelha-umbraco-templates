using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.DataList;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Links;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;

public interface IRedirectSettingsProperties
{
    public IReadOnlyCollection<ApiLink>? RedirectLink { get; init; }
    public RedirectFallbackDirection? RedirectDirection { get; init; }
}

public abstract class RedirectSettingsProperties : IRedirectSettingsProperties
{
    public IReadOnlyCollection<ApiLink>? RedirectLink { get; init; }
    public RedirectFallbackDirection? RedirectDirection { get; init; }
}
