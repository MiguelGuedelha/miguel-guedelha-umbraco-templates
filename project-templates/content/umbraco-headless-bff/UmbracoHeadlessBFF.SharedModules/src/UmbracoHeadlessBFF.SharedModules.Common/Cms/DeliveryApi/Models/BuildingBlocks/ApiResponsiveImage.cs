using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Media;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiResponsiveImage : ApiElement<ApiResponsiveImageProperties>
{
}

public sealed class ApiResponsiveImageProperties
{
    public IReadOnlyCollection<ApiMediaWithCrops>? MainImage { get; init; }
    public ApiMediaWithCrops? MainImageItem => MainImage?.FirstOrDefault();
    public IReadOnlyCollection<ApiMediaWithCrops>? MobileImage { get; init; }
    public ApiMediaWithCrops? MobileImageItem => MobileImage?.FirstOrDefault();
    public string? AltText { get; init; }
}
