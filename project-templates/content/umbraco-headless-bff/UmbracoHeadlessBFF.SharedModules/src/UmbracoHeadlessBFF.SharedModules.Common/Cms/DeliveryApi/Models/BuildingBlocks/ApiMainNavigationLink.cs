using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiMainNavigationLink : IApiElement<ApiMainNavigationLinkProperties>
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required ApiMainNavigationLinkProperties Properties { get; init; }
}

public sealed class ApiMainNavigationLinkProperties
{
    public IReadOnlyCollection<ApiLink>? Link { get; init; }
    public ApiBlockList<ApiLinkWithSublinks>? SubLinks { get; init; }
}
