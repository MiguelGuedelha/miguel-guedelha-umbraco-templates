using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed record ApiResponsiveImage : IApiElement<ApiResponsiveImageProperties>
{
    public required Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ElementTypes.ApiResponsiveImage;
    public required ApiResponsiveImageProperties Properties { get; init; }
}

public sealed record ApiResponsiveImageProperties
{
    public IReadOnlyCollection<ApiMediaWithCrops>? MainImage { get; init; }
    public IReadOnlyCollection<ApiMediaWithCrops>? MobileImage { get; init; }
    public string? AltText { get; init; }
}
