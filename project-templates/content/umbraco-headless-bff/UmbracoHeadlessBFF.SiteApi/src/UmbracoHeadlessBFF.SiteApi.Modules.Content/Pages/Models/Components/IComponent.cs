namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Components;

internal interface IComponent
{
    Guid Id { get; init; }
    string ContentType { get; init; }
}

internal abstract class Component : IComponent
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
}
