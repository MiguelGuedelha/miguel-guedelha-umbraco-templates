using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;

namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.BuildingBlocks;

public sealed record ApiMainNavigationLink : IApiElement<ApiMainNavigationLinkProperties>
{
    public required Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ElementTypes.ApiMainNavigationLink;
    public required ApiMainNavigationLinkProperties Properties { get; init; }
}

public sealed record ApiMainNavigationLinkProperties
{
    public IReadOnlyCollection<ApiLink>? Link { get; init; }
    public ApiBlockList<ApiLinkWithSublinks>? SubLinks { get; init; }
}
