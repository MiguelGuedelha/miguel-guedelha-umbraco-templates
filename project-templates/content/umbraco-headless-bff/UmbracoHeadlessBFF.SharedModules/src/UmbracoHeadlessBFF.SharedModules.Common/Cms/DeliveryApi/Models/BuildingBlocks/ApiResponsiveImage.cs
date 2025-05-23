using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiResponsiveImage : ApiElement<ApiResponsiveImageProperties>
{
}

public sealed class ApiResponsiveImageProperties
{
    public IReadOnlyCollection<ApiMediaWithCrops>? MainImage { get; init; }
    public IReadOnlyCollection<ApiMediaWithCrops>? MobileImage { get; init; }
    public string? AltText { get; init; }
}
