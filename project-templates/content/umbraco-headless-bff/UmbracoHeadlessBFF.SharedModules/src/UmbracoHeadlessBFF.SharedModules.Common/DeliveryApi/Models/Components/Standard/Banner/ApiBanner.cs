using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.BlockList;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Standard.Banner;

public sealed class ApiBanner : ApiElement<ApiBannerProperties>
{
    public const string ContentType = "banner";
}

public sealed class ApiBannerProperties : IApiJumpMenuConfiguration
{
    public string? JumpMenuHeading { get; init; }
    public string? JumpMenuAnchorId { get; init; }
    public ApiBlockList<ApiBannerItem>? Items { get; init; }
}
