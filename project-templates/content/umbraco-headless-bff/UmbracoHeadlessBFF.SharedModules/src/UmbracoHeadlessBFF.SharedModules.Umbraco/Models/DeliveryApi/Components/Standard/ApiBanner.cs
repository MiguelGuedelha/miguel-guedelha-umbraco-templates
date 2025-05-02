using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Components.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.BlockList;
using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.RichText;
using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.ModelsBuilder;

namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Components.Standard;

public sealed class ApiBanner : IApiElement
{
    public required string Id { get; init; }
    public string ContentType => Banner.ModelTypeAlias;
    public required ApiBannerProperties Properties { get; init; }
}

public sealed class ApiBannerProperties : IApiHeadingWithSubHeading, IApiRteDescription, IApiJumpMenuConfiguration
{
    public string? JumpMenuHeading { get; init; }
    public string? JumpMenuAnchorId { get; init; }
    public string? Heading { get; init; }
    public string HeadingSize { get; init; } = "h1";
    public string? SubHeading { get; init; }
    public ApiRichTextItem? Description { get; init; }
    public ApiBlockList? BackgroundMedia { get; init; }
}
