namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Layouts.Abstractions;

internal interface ILayout
{
    Guid Id { get; init; }
    string ContentType { get; init; }
}

public abstract class Layout : ILayout
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
}
