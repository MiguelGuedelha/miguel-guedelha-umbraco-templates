using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Data.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Data.BlockGrid;

public sealed class ApiBlockGridItem : IApiBlockGridItem
{
    public int RowSpan { get; init; }
    public int ColumnSpan { get; init; }
    public int AreaGridColumns { get; init; }
    public ICollection<ApiBlockGridArea> Areas { get; init; } = [];
    public required IApiElement Content { get; init; }
    public IApiElement? Settings { get; init; }
}
