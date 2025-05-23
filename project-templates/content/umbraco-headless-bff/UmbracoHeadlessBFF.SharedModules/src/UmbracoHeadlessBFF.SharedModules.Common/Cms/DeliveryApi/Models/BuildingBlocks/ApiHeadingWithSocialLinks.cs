using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiHeadingWithSocialLinks : IApiElement<ApiHeadingWithSocialLinksProperties>
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required ApiHeadingWithSocialLinksProperties Properties { get; init; }
}

public sealed class ApiHeadingWithSocialLinksProperties : IApiHeading
{
    public string? Heading { get; init; }
    public string HeadingSize { get; init; } = "p";
    public IReadOnlyCollection<ApiSocialLink>? SocialLinks { get; init; }
}
