namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Pages;

internal interface IPage
{
    Guid Id { get; init; }
    string ContentType { get; init; }
    PageContext Context { get; init; }
}

internal abstract class Page : IPage
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required PageContext Context { get; init; }
}

internal abstract class Page<T> : IPage
    where T : IAdditionalProperties
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required PageContext Context { get; init; }
    public required PageContent<T> Content { get; init; }
}
