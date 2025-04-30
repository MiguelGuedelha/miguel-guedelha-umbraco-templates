using UmbracoHeadlessBFF.SharedModules.Umbraco.Models;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Components.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Data.BlockList;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Components.Standard;

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
    public string? Markup { get; init; }
    public ApiBlockList? BackgroundMedia { get; init; }
}
