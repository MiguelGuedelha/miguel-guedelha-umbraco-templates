using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Compositions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed record ApiHeadingWithLinks : IApiElement<ApiHeadingWithLinksProperties>
{
    public required Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ElementTypes.ApiHeadingWithLinks;
    public required ApiHeadingWithLinksProperties Properties { get; init; }
}

public sealed record ApiHeadingWithLinksProperties : IApiHeading
{
    public string? Heading { get; init; }
    public string HeadingSize { get; init; } = "p";
    public IReadOnlyCollection<ApiLink>? Links { get; init; }
}
