using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;

public sealed class ApiBlogListing : IApiElement<ApiBlogListingProperties>
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required ApiBlogListingProperties Properties { get; init; }
}

public sealed class ApiBlogListingProperties : IApiJumpMenuConfiguration, IApiHeadingWithSubHeading
{
    public string? JumpMenuHeading { get; init; }
    public string? JumpMenuAnchorId { get; init; }
    public string? Heading { get; init; }
    public string HeadingSize { get; init; } = "h2";
    public string? SubHeading { get; init; }
}
