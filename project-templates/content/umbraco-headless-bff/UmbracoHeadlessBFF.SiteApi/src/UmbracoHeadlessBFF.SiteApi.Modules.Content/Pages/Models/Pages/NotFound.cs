namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Pages;

internal sealed class NotFound : IPage<EmptyAdditionalProperties>
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required PageContext Context { get; init; }
    public required PageContent<EmptyAdditionalProperties> Content { get; init; }
}
