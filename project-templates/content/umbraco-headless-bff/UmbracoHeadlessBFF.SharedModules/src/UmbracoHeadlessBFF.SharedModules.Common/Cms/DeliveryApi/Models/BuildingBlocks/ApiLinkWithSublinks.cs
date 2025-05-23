using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiLinkWithSublinks : ApiElement<ApiLinkWithSublinksProperties>
{
}

public sealed class ApiLinkWithSublinksProperties
{
    public IReadOnlyCollection<ApiLink>? Link { get; init; }
    public IReadOnlyCollection<ApiLink>? SubLinks { get; init; }
}
