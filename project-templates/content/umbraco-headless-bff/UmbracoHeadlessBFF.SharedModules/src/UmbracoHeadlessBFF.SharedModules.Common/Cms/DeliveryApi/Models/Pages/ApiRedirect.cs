using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;

public sealed class ApiRedirect : ApiContent<ApiRedirectProperties>
{
}

public sealed class ApiRedirectProperties : RedirectSettingsProperties
{
}
