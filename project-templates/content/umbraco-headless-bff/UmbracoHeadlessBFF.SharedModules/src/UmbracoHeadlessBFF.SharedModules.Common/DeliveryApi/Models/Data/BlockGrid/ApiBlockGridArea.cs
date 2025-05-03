namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.BlockGrid;

public sealed class ApiBlockGridArea
{
    public required string Alias { get; init; }
    public int RowSpan { get; init; }
    public int ColumnSpan { get; init; }
    public IReadOnlyCollection<ApiBlockGridItem> Items { get; init; } = [];
}
