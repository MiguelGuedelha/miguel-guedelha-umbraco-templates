using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiMainNavigationLink : ApiElement<ApiMainNavigationLinkProperties>
{
}

public sealed class ApiMainNavigationLinkProperties
{
    public IReadOnlyCollection<ApiLink>? Link { get; init; }
    public ApiBlockList<ApiLinkWithSublinks>? SubLinks { get; init; }
}
