using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiResponsiveImage : IApiElement<ApiResponsiveImageProperties>
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required ApiResponsiveImageProperties Properties { get; init; }
}

public sealed class ApiResponsiveImageProperties
{
    public IReadOnlyCollection<ApiMediaWithCrops>? MainImage { get; init; }
    public IReadOnlyCollection<ApiMediaWithCrops>? MobileImage { get; init; }
    public string? AltText { get; init; }
}
