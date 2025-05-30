namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages;

internal sealed record BlogArticle : IPage<EmptyAdditionalProperties>
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required PageContext Context { get; init; }
    public required PageContent<EmptyAdditionalProperties> Content { get; init; }
}
