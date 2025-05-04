using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Links;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiHeadingWithLinks : ApiElement<ApiHeadingWithLinksProperties>
{
    public const string ContentType = "headingWithLinks";
}

public sealed class ApiHeadingWithLinksProperties : IApiHeading
{
    public string? Heading { get; init; }
    public string HeadingSize { get; init; } = "p";
    public IReadOnlyCollection<ApiLink>? Links { get; init; }
}
