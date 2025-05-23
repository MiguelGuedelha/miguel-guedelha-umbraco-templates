using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;

public sealed class ApiGallerySettings : ApiElement<ApiGallerySettingsProperties>
{
}

public sealed class ApiGallerySettingsProperties
{
    public int CardsPerRow { get; init; }
}
