using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Media;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.BuildingBlocks;

public class ApiResponsiveImage : ApiElement
{
    public const string ContentType = "responsiveImage";
    public required ApiResponsiveImageProperties Properties { get; init; }
}

public class ApiResponsiveImageProperties
{
    public IReadOnlyCollection<ApiMediaWithCrops>? MainImage { get; init; }
    public IReadOnlyCollection<ApiMediaWithCrops>? MobileImage { get; init; }
    public string? AltText { get; init; }
}
