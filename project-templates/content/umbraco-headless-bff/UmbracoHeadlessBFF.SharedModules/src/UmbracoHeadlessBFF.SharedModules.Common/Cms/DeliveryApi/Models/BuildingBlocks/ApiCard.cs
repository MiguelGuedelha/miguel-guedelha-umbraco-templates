using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiCard : ApiElement<ApiCardProperties>
{
}

public sealed class ApiCardProperties : IApiHeadingWithSubHeading
{
    public string? Heading { get; init; }
    public string HeadingSize { get; init; } = "h3";
    public string? SubHeading { get; init; }
    public ApiBlockList<ApiResponsiveImage>? Image { get; init; }
    public IReadOnlyCollection<ApiLink>? Link { get; init; }
}
