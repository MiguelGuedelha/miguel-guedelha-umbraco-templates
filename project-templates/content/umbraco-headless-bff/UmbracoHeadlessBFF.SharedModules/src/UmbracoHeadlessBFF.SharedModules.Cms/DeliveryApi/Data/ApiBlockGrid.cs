namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;

public sealed record ApiBlockGrid
{
    public int GridColumns { get; init; }
    public IReadOnlyCollection<ApiBlockGridItem> Items { get; init; } = [];
}
