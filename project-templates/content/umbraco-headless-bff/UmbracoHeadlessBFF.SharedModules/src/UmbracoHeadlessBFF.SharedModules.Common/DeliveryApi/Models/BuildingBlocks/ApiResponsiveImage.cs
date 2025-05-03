using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Media;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.BuildingBlocks;

public class ApiResponsiveImage : IApiElement
{
    public required string Id { get; init; }
    public string ContentType => "responsiveImage";
    public required ApiResponsiveImageProperties Properties { get; init; }
}

public class ApiResponsiveImageProperties
{
    public ICollection<ApiMediaWithCrops>? MainImage { get; init; }
    public ICollection<ApiMediaWithCrops>? MobileImage { get; init; }
    public string? AltText { get; init; }
}
