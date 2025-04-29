namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Blocks.Abstractions;

public interface IApiBlockModel<TContent, TSettings>
    where TContent : class, IApiElementModel
    where TSettings : class, IApiElementModel
{
    TContent Content { get; init; }
    TSettings Settings { get; init; }
}

public interface IApiBlockModel<TContent>
    where TContent : class, IApiElementModel
{
    TContent Content { get; init; }
}

public interface IApiBlockModel
{
    IApiElementModel Content { get; init; }
    IApiElementModel? Settings { get; init; }
}
