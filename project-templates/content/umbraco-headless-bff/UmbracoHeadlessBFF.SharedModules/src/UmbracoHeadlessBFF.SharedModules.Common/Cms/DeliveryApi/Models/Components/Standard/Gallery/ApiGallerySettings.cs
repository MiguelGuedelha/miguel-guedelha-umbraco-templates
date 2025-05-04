using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Standard.Gallery;

public sealed class ApiGallerySettings : ApiElement<ApiGallerySettingsProperties>
{
    public const string ContentType = "gallerySettings";
}

public sealed class ApiGallerySettingsProperties
{
    public int CardsPerRow { get; init; }
}
