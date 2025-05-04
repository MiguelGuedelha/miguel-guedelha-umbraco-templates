using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Links;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiLinkWithSublinks : ApiElement<ApiLinkWithSublinksProperties>
{
    public const string ContentType = "linkWithSubLinks";
}

public sealed class ApiLinkWithSublinksProperties
{
    public IReadOnlyCollection<ApiLink>? Link { get; init; }
    public ApiLink? LinkItem => Link?.FirstOrDefault();
    public IReadOnlyCollection<ApiLink>? SubLinks { get; init; }
}
