namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages;

internal interface IPage
{
    Guid Id { get; init; }
    string ContentType { get; init; }
    PageContext Context { get; init; }
}

internal interface IPage<T> : IPage
    where T : IAdditionalProperties
{
    public PageContent<T> Content { get; init; }
}
