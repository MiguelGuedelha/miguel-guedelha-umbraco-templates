using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.BlockList;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Links;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiCard : ApiElement
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
    public IReadOnlyCollection<ApiLinkModel>? Link { get; init; }
    public ApiLinkModel? LinkItem => Link?.FirstOrDefault();
}
