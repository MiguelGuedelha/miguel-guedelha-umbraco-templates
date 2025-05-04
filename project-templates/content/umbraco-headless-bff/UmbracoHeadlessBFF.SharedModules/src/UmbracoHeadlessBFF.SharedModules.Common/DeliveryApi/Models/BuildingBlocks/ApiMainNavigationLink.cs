using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.BlockList;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Links;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiMainNavigationLink : ApiElement<ApiMainNavigationLinkProperties>
{
    public const string ContentType = "mainNavigationLink";
}

public sealed class ApiMainNavigationLinkProperties
{
    public IReadOnlyCollection<ApiLink>? Link { get; init; }
    public ApiLink? LinkItem => Link?.FirstOrDefault();
    public ApiBlockList<ApiLinkWithSublinks>? SubLinks { get; init; }
}
