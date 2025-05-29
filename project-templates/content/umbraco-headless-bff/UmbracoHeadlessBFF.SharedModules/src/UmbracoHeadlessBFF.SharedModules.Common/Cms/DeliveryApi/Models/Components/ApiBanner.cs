using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Compositions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;

public sealed record ApiBanner : IApiElement<ApiBannerProperties>
{
    public required Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ElementTypes.ApiBanner;
    public required ApiBannerProperties Properties { get; init; }
}

public sealed record ApiBannerProperties : IApiJumpMenuConfiguration
{
    public string? JumpMenuHeading { get; init; }
    public string? JumpMenuAnchorId { get; init; }
    public ApiBlockList<ApiBannerItem>? Items { get; init; }
}
