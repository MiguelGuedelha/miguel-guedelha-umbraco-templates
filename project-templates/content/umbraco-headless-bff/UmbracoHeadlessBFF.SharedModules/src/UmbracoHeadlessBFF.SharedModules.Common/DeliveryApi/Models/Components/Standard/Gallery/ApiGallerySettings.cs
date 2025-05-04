using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Standard.Gallery;

public sealed class ApiGallerySettings : ApiElement<ApiGallerySettingsProperties>
{
    public const string ContentType = "gallerySettings";
}

public sealed class ApiGallerySettingsProperties
{
    public int CardsPerRow { get; init; }
}
