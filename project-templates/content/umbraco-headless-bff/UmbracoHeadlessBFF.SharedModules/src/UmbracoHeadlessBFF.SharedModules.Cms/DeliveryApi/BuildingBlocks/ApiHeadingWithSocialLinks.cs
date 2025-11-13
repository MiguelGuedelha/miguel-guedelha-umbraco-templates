using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Components.Compositions;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;

namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.BuildingBlocks;

public sealed record ApiHeadingWithSocialLinks : IApiElement<ApiHeadingWithSocialLinksProperties>
{
    public required Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ElementTypes.ApiHeadingWithSocialLinks;
    public required ApiHeadingWithSocialLinksProperties Properties { get; init; }
}

public sealed record ApiHeadingWithSocialLinksProperties : IApiHeading
{
    public string? Heading { get; init; }
    public string HeadingSize { get; init; } = "p";
    public IReadOnlyCollection<ApiSocialLink>? SocialLinks { get; init; }
}
