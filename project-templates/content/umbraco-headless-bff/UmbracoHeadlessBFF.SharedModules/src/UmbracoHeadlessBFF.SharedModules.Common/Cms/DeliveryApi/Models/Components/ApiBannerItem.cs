using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Compositions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;

public sealed record ApiBannerItem : IApiElement<ApiBannerItemProperties>
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required ApiBannerItemProperties Properties { get; init; }
}

public sealed record ApiBannerItemProperties : IApiHeadingWithSubHeading, IApiRteDescription
{
    public string? Heading { get; init; }
    public string HeadingSize { get; init; } = "h2";
    public string? SubHeading { get; init; }
    public ApiRichTextItem? Description { get; init; }
    public ApiBlockList? BackgroundMedia { get; init; }
}
