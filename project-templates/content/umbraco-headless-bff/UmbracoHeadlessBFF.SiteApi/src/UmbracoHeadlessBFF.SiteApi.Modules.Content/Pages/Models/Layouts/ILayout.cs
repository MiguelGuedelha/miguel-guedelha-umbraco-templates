namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Layouts;

internal interface ILayout
{
    Guid Id { get; init; }
    string ContentType { get; init; }
}

internal abstract class Layout : ILayout
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
}
