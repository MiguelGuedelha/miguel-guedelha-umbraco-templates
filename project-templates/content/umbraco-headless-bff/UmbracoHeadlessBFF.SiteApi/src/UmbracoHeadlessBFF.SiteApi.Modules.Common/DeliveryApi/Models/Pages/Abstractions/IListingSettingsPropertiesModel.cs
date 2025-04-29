using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Blocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Pages.Abstractions;

public interface IListingSettingsPropertiesModel
{
    bool? ExcludeFromSearch { get; init; }
    string? ListingTitle { get; init; }
    string? ListingDescription { get; init; }
    ApiBlockListModel ListingImage { get; init; }
}
