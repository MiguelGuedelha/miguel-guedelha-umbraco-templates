using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Compositions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;

public sealed record ApiFullWidthImage : IApiElement<ApiFullWidthImageProperties>
{
    public required Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ElementTypes.ApiFullWidthImage;
    public required ApiFullWidthImageProperties Properties { get; init; }
}

public sealed record ApiFullWidthImageProperties : IApiJumpMenuConfiguration
{
    public string? JumpMenuHeading { get; init; }
    public string? JumpMenuAnchorId { get; init; }
    public ApiBlockList<ApiResponsiveImage>? Image { get; init; }
}
