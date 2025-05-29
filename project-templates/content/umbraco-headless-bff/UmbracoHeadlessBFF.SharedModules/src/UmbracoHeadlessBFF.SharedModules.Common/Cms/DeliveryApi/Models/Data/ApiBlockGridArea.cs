namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

public sealed record ApiBlockGridArea
{
    public required string Alias { get; init; }
    public int RowSpan { get; init; }
    public int ColumnSpan { get; init; }
    public IReadOnlyCollection<ApiBlockGridItem> Items { get; init; } = [];
}
