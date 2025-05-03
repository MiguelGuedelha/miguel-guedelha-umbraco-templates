using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Links;

public sealed class ApiLinkModel
{
    public string? Url { get; init; }
    public string? QueryString { get; init; }
    public string? Title { get; init; }
    public string? Target { get; init; }
    public Guid? DestinationId { get; init; }
    public string? DestinationType { get; init; }
    public ApiContentRoute? Route { get; init; }
    public LinkType LinkType { get; init; }
}
