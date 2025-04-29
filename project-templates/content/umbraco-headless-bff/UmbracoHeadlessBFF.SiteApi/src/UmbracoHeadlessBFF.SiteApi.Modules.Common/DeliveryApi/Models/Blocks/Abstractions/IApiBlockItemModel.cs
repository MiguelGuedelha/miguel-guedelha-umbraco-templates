namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Blocks.Abstractions;

public interface IApiBlockItemModel<TContent, TSettings> : IApiBlockModel<TContent, TSettings>
    where TContent : class, IApiElementModel
    where TSettings : class, IApiElementModel
{
}

public interface IApiBlockItemModel<TContent> : IApiBlockModel<TContent>
    where TContent : class, IApiElementModel
{
}

public interface IApiBlockItemModel : IApiBlockModel
{
}
