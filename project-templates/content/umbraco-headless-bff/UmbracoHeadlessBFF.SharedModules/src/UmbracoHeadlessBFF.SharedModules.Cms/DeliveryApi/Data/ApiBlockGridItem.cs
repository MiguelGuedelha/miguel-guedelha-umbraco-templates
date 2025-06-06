﻿namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;

public sealed record ApiBlockGridItem : IApiBlockGridItem
{
    public int RowSpan { get; init; }
    public int ColumnSpan { get; init; }
    public int AreaGridColumns { get; init; }
    public IReadOnlyCollection<ApiBlockGridArea> Areas { get; init; } = [];
    public required IApiElement Content { get; init; }
    public IApiElement? Settings { get; init; }
}
