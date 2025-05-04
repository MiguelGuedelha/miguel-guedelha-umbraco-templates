using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Settings;

public sealed class ApiSiteDictionary : ApiContent<ApiSiteDictionaryProperties>
{
    public const string ContentType = "siteDictionary";
}

public sealed class ApiSiteDictionaryProperties
{
    public string? GeneralButtonsNextText { get; init; }
    public string? GeneralButtonsBackText { get; init; }
    public string? GeneralSearchPlaceholderText { get; init; }
}
