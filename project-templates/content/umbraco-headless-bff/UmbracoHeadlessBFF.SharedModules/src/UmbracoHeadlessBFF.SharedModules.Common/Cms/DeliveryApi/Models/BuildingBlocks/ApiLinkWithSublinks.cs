using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed record ApiLinkWithSublinks : IApiElement<ApiLinkWithSublinksProperties>
{
    public required Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ElementTypes.ApiLinkWithSublinks;
    public required ApiLinkWithSublinksProperties Properties { get; init; }
}

public sealed record ApiLinkWithSublinksProperties
{
    public IReadOnlyCollection<ApiLink>? Link { get; init; }
    public IReadOnlyCollection<ApiLink>? SubLinks { get; init; }
}
