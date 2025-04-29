using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Blocks.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Blocks;

public sealed class ApiBlockGridItemModel : IApiBlockGridItemModel
{
    public int RowSpan { get; init; }
    public int ColumnSpan { get; init; }
    public int AreaGridColumns { get; init; }
    public ICollection<ApiBlockGridAreaModel> Areas { get; init; } = [];
    public required IApiElementModel Content { get; init; }
    public IApiElementModel? Settings { get; init; }
}
