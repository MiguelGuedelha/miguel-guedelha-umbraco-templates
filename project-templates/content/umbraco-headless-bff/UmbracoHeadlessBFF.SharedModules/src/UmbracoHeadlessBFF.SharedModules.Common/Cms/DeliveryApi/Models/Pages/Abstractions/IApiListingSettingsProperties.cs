using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.BlockList;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;

public interface IApiListingSettingsProperties
{
    bool ExcludeFromSearch { get; init; }
    string? ListingTitle { get; init; }
    string? ListingDescription { get; init; }
    ApiBlockList<ApiResponsiveImage>? ListingImage { get; init; }
    ApiResponsiveImage? ListingImageItem => ListingImage?.Items.FirstOrDefault()?.Content;
}

public abstract class ApiListingSettingsProperties : IApiListingSettingsProperties
{
    public bool ExcludeFromSearch { get; init; }
    public string? ListingTitle { get; init; }
    public string? ListingDescription { get; init; }
    public ApiBlockList<ApiResponsiveImage>? ListingImage { get; init; }
}
