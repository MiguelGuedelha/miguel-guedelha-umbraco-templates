using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;

public sealed class ApiBanner : IApiElement<ApiBannerProperties>
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required ApiBannerProperties Properties { get; init; }
}

public sealed class ApiBannerProperties : IApiJumpMenuConfiguration
{
    public string? JumpMenuHeading { get; init; }
    public string? JumpMenuAnchorId { get; init; }
    public ApiBlockList<ApiBannerItem>? Items { get; init; }
}
