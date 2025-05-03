using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.BlockList;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Standard;

public sealed class ApiGallery : ApiElement
{
    public const string ContentType = "gallery";
    public required ApiGalleryProperties Properties { get; init; }
}

public sealed class ApiGalleryProperties : IApiHeadingWithSubHeading
{
    public string? Heading { get; init; }
    public string HeadingSize { get; init; } = "h2";
    public string? SubHeading { get; init; }
    public ApiBlockList<ApiCard>? Items { get; init; }
}

public sealed class ApiGallerySettings : ApiElement
{
    public const string ContentType = "gallerySettings";
    public required ApiGallerySettingsProperties Properties { get; init; }
}

public sealed class ApiGallerySettingsProperties
{
    public int CardsPerRow { get; init; }
}
