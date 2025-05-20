using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Settings;

public sealed class ApiSiteDictionary : ApiContent<ApiSiteDictionaryProperties>
{
}

public sealed class ApiSiteDictionaryProperties : RedirectSettingsProperties
{
    public string? GeneralButtonsNextText { get; init; }
    public string? GeneralButtonsBackText { get; init; }
    public string? GeneralSearchPlaceholderText { get; init; }
}
