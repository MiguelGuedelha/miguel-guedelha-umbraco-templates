using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.BlockList;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Standard;

public sealed class ApiCarousel : ApiElement<ApiCarouselProperties>
{
    public const string ContentType = "carousel";
}

public sealed class ApiCarouselProperties : IApiJumpMenuConfiguration, IApiHeadingWithSubHeading
{
    public string? JumpMenuHeading { get; init; }
    public string? JumpMenuAnchorId { get; init; }
    public string? Heading { get; init; }
    public string HeadingSize { get; init; } = "h2";
    public string? SubHeading { get; init; }
    public ApiBlockList<ApiCard>? Cards { get; init; }
}
