using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Media;
using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.ModelsBuilder;

namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.BuildingBlocks;

public class ApiResponsiveImage : IApiElement
{
    public required string Id { get; init; }
    public string ContentType => ResponsiveImage.ModelTypeAlias;
    public required ApiResponsiveImageProperties Properties { get; init; }
}

public class ApiResponsiveImageProperties
{
    public ICollection<ApiMediaWithCrops>? MainImage { get; init; }
    public ICollection<ApiMediaWithCrops>? MobileImage { get; init; }
    public string? AltText { get; init; }
}
