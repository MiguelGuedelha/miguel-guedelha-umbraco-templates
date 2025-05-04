using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.BlockList;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Standard;

public sealed class ApiFullWidthImage : ApiElement<ApiFullWidthImageProperties>
{
    public const string ContentType = "fullWidthImage";
}

public sealed class ApiFullWidthImageProperties : IApiJumpMenuConfiguration
{
    public string? JumpMenuHeading { get; init; }
    public string? JumpMenuAnchorId { get; init; }
    public ApiBlockList<ApiResponsiveImage>? Image { get; init; }
    public ApiResponsiveImage? ImageItem => Image?.Items.FirstOrDefault()?.Content;
}
