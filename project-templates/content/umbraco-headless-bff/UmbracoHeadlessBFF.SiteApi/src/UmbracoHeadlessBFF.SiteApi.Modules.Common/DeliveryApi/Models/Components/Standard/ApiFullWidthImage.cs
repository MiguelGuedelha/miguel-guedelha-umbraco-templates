using UmbracoHeadlessBFF.SharedModules.Umbraco.Models;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Components.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Data.BlockList;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Components.Standard;

public class ApiFullWidthImage : IApiElement
{
    public required string Id { get; init; }
    public string ContentType => FullWidthImage.ModelTypeAlias;
    public required ApiFullWidthImageProperties Properties { get; init; }
}

public sealed class ApiFullWidthImageProperties : IApiJumpMenuConfiguration
{
    public string? JumpMenuHeading { get; init; }
    public string? JumpMenuAnchorId { get; init; }
    public ApiBlockList<ApiResponsiveImage>? Image { get; init; }
}
