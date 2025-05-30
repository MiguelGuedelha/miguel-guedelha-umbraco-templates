namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;

internal interface IComponent
{
    Guid Id { get; init; }
    string ContentType { get; init; }
}
