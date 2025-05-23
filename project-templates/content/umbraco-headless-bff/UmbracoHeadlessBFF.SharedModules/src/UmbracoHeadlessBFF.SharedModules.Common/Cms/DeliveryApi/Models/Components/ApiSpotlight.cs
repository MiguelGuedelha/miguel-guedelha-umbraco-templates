using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;

public sealed class ApiSpotlight : IApiElement<ApiSpotlightProperties>
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required ApiSpotlightProperties Properties { get; init; }
}

public sealed class ApiSpotlightProperties : IApiJumpMenuConfiguration, IApiHeading, IApiRteDescription
{
    public string? JumpMenuHeading { get; init; }
    public string? JumpMenuAnchorId { get; init; }
    public string? Heading { get; init; }
    public string HeadingSize { get; init; } = "h2";
    public ApiRichTextItem? Description { get; init; }
    public IReadOnlyCollection<ApiLink>? Cta { get; init; }
    public ApiBlockList? Media { get; init; }
}
