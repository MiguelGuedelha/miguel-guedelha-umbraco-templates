namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;

public interface IApiBlockListItem<TContent, TSettings> : IApiBlock<TContent, TSettings>
    where TContent : class, IApiElement
    where TSettings : class, IApiElement
{
}

public interface IApiBlockListItem<TContent> : IApiBlock<TContent>
    where TContent : class, IApiElement
{
}

public interface IApiBlockListItem : IApiBlock
{
}
