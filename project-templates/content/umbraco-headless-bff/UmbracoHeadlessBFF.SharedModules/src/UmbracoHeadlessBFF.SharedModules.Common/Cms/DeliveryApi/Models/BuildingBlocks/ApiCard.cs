using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.BlockList;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Links;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiCard : ApiElement<ApiCardProperties>
{
    public const string ContentType = "card";
}

public sealed class ApiCardProperties : IApiHeadingWithSubHeading
{
    public string? Heading { get; init; }
    public string HeadingSize { get; init; } = "h3";
    public string? SubHeading { get; init; }
    public ApiBlockList<ApiResponsiveImage>? Image { get; init; }
    public ApiResponsiveImage? ImageItem => Image?.Items.FirstOrDefault()?.Content;
    public IReadOnlyCollection<ApiLink>? Link { get; init; }
    public ApiLink? LinkItem => Link?.FirstOrDefault();
}
