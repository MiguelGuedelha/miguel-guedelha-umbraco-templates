namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.BlockGrid;

public sealed class ApiBlockGrid
{
    public int GridColumns { get; init; }
    public IReadOnlyCollection<ApiBlockGridItem> Items { get; set; } = [];
}
