namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Blocks.Abstractions;

public interface IApiBlockGridItemModel<TContent, TSettings> : IApiBlockGridItemModelExtendedProperties, IApiBlockModel<TContent, TSettings>
    where TContent : class, IApiElementModel
    where TSettings : class, IApiElementModel
{
}

public interface IApiBlockGridItemModel<TContent> : IApiBlockGridItemModelExtendedProperties, IApiBlockModel<TContent>
    where TContent : class, IApiElementModel
{
}

public interface IApiBlockGridItemModel : IApiBlockGridItemModelExtendedProperties, IApiBlockModel
{
}

public interface IApiBlockGridItemModelExtendedProperties
{
    int RowSpan { get; init; }
    int ColumnSpan { get; init; }
    int AreaGridColumns { get; init; }
    ICollection<ApiBlockGridAreaModel> Areas { get; init; }
}
