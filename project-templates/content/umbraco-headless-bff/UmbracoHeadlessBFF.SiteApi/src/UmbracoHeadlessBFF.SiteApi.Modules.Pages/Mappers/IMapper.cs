namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers;

internal interface IMapper<in TIn, TOut>
{
    Task<TOut?> Map(TIn model);
}

internal interface IMapper<in TModel, in TSettings, TOut>
{
    Task<TOut?> Map(TModel model, TSettings? settings);
}
