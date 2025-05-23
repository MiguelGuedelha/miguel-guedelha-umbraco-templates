using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;

public interface IApiListingSettingsProperties
{
    bool ExcludeFromSearch { get; init; }
    string? ListingTitle { get; init; }
    string? ListingDescription { get; init; }
    ApiBlockList<ApiResponsiveImage>? ListingImage { get; init; }
}
