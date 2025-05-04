using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Media;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiResponsiveImage : ApiElement<ApiResponsiveImageProperties>
{
    public const string ContentType = "responsiveImage";
}

public sealed class ApiResponsiveImageProperties
{
    public IReadOnlyCollection<ApiMediaWithCrops>? MainImage { get; init; }
    public ApiMediaWithCrops? MainImageItem => MainImage?.FirstOrDefault();
    public IReadOnlyCollection<ApiMediaWithCrops>? MobileImage { get; init; }
    public ApiMediaWithCrops? MobileImageItem => MobileImage?.FirstOrDefault();
    public string? AltText { get; init; }
}
