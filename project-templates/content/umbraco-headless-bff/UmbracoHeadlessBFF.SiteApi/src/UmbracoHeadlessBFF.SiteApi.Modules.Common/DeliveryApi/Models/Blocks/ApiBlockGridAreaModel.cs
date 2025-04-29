namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Blocks;

public sealed class ApiBlockGridAreaModel
{
    public required string Alias { get; init; }
    public int RowSpan { get; init; }
    public int ColumnSpan { get; init; }
    public ICollection<ApiBlockGridItemModel> Items { get; init; } = [];
}
