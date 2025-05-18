namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components.Abstractions;

internal interface IComponent
{
    string Id { get; init; }
    string ContentType { get; init; }
}

public abstract class Component : IComponent
{
    public required string Id { get; init; }
    public required string ContentType { get; init; }
}
