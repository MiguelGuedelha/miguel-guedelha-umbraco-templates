using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Compositions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;

public sealed record ApiGallery : IApiElement<ApiGalleryProperties>
{
    public required Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ElementTypes.ApiGallery;
    public required ApiGalleryProperties Properties { get; init; }
}

public sealed record ApiGalleryProperties : IApiHeadingWithSubHeading
{
    public string? Heading { get; init; }
    public string HeadingSize { get; init; } = "h2";
    public string? SubHeading { get; init; }
    public ApiBlockList<ApiCard>? Items { get; init; }
}
