namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers;

internal interface IMapper<in TIn, TOut>
{
    Task<TOut?> Map(TIn model);
}

internal interface IMapper<in TModel, in TSettings, TOut>
{
    Task<TOut?> Map(TModel model, TSettings? settings);
}
