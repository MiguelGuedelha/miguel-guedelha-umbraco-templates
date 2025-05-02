using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Components.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.BlockList;
using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.ModelsBuilder;

namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Components.Standard;

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
