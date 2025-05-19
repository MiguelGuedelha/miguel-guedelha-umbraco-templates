namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Abstractions;

internal interface IPage
{
}

internal interface IPage<T> : IPage
    where T : IAdditionalProperties
{
    PageContext Context { get; init; }
    PageContent<T> Content { get; init; }
}

internal abstract class Page<T> : IPage<T>
    where T : IAdditionalProperties
{
    public required PageContext Context { get; init; }
    public required PageContent<T> Content { get; init; }
}
