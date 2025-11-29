namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages;

public sealed record ApiContentReference : IApiContent
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required string Name { get; init; }
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
    public required ApiContentRoute Route { get; init; }
    public Dictionary<string, ApiContentRoute> Cultures { get; init; } = [];
}
