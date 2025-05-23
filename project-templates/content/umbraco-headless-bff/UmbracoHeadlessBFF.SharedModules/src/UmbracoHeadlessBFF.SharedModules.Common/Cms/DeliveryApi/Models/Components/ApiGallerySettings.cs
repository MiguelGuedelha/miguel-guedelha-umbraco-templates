using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;

public sealed class ApiGallerySettings : IApiElement<ApiGallerySettingsProperties>
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required ApiGallerySettingsProperties Properties { get; init; }
}

public sealed class ApiGallerySettingsProperties
{
    public int CardsPerRow { get; init; }
}
