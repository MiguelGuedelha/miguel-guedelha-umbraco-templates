using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

public sealed class ApiLink
{
    public string? Url { get; init; }
    public string? QueryString { get; init; }
    public string? Title { get; init; }
    public string? Target { get; init; }
    public Guid? DestinationId { get; init; }
    public string? DestinationType { get; init; }
    public ApiContentRoute? Route { get; init; }
    public ApiLinkType LinkType { get; init; }
}
