using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Data.BlockList;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Pages.Abstractions;

public interface IApiListingSettingsProperties
{
    bool? ExcludeFromSearch { get; init; }
    string? ListingTitle { get; init; }
    string? ListingDescription { get; init; }
    ApiBlockList ListingImage { get; init; }
}
