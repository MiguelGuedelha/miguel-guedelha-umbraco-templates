using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiCard : IApiElement<ApiCardProperties>
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required ApiCardProperties Properties { get; init; }
}

public sealed class ApiCardProperties : IApiHeadingWithSubHeading
{
    public string? Heading { get; init; }
    public string HeadingSize { get; init; } = "h3";
    public string? SubHeading { get; init; }
    public ApiBlockList<ApiResponsiveImage>? Image { get; init; }
    public IReadOnlyCollection<ApiLink>? Link { get; init; }
}
