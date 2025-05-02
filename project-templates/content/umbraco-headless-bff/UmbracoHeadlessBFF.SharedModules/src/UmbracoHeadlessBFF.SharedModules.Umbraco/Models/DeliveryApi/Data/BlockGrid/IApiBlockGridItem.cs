using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.BlockGrid;

public interface IApiBlockGridItem<TContent, TSettings> : IApiBlockGridItemProperties, IApiBlock<TContent, TSettings>
    where TContent : class, IApiElement
    where TSettings : class, IApiElement
{
}

public interface IApiBlockGridItem<TContent> : IApiBlockGridItemProperties, IApiBlock<TContent>
    where TContent : class, IApiElement
{
}

public interface IApiBlockGridItem : IApiBlockGridItemProperties, IApiBlock
{
}

public interface IApiBlockGridItemProperties
{
    int RowSpan { get; init; }
    int ColumnSpan { get; init; }
    int AreaGridColumns { get; init; }
    ICollection<ApiBlockGridArea> Areas { get; init; }
}
