using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Gallery;

public sealed class ApiGallerySettings : ApiElement<ApiGallerySettingsProperties>
{
}

public sealed class ApiGallerySettingsProperties
{
    public int CardsPerRow { get; init; }
}
