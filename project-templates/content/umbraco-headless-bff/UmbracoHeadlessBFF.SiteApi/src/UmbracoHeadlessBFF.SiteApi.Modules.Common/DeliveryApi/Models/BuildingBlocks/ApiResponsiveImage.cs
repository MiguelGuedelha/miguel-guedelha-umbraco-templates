using UmbracoHeadlessBFF.SharedModules.Umbraco.Models;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Media;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.BuildingBlocks;

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
