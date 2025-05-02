using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.BlockList;

namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Pages.Abstractions;

public interface IApiListingSettingsProperties
{
    bool? ExcludeFromSearch { get; init; }
    string? ListingTitle { get; init; }
    string? ListingDescription { get; init; }
    ApiBlockList ListingImage { get; init; }
}
