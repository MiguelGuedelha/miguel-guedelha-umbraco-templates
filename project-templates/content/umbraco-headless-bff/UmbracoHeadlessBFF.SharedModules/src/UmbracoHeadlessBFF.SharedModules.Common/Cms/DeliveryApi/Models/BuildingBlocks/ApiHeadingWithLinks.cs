using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiHeadingWithLinks : IApiElement<ApiHeadingWithLinksProperties>
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required ApiHeadingWithLinksProperties Properties { get; init; }
}

public sealed class ApiHeadingWithLinksProperties : IApiHeading
{
    public string? Heading { get; init; }
    public string HeadingSize { get; init; } = "p";
    public IReadOnlyCollection<ApiLink>? Links { get; init; }
}
