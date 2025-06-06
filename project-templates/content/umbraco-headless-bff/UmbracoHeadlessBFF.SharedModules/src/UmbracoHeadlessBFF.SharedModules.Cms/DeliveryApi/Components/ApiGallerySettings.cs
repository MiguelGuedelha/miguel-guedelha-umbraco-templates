﻿using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;

namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Components;

public sealed record ApiGallerySettings : IApiElement<ApiGallerySettingsProperties>
{
    public required Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ElementTypes.ApiGallerySettings;
    public required ApiGallerySettingsProperties Properties { get; init; }
}

public sealed record ApiGallerySettingsProperties
{
    public int CardsPerRow { get; init; }
}
