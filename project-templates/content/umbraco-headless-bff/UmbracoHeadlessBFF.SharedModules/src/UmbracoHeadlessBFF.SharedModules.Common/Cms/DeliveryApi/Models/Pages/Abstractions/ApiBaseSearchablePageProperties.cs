using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.BlockList;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;

public abstract class ApiBaseSearchablePageProperties : ApiBaseNonSearchablePageProperties, IApiListingSettingsProperties
{
    public bool ExcludeFromSearch { get; init; }
    public string? ListingTitle { get; init; }
    public string? ListingDescription { get; init; }
    public ApiBlockList<ApiResponsiveImage>? ListingImage { get; init; }
}
