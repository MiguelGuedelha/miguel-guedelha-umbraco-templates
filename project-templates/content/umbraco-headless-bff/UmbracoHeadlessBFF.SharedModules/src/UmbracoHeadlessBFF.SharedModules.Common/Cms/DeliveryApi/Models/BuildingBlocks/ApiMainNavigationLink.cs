using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.BlockList;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Links;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiMainNavigationLink : ApiElement<ApiMainNavigationLinkProperties>
{
}

public sealed class ApiMainNavigationLinkProperties
{
    public IReadOnlyCollection<ApiLink>? Link { get; init; }
    public ApiLink? LinkItem => Link?.FirstOrDefault();
    public ApiBlockList<ApiLinkWithSublinks>? SubLinks { get; init; }
}
