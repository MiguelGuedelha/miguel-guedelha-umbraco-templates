namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

public sealed record ApiBlockGrid
{
    public int GridColumns { get; init; }
    public IReadOnlyCollection<ApiBlockGridItem> Items { get; set; } = [];
}
