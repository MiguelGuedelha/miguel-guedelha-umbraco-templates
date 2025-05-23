namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Components;

internal interface IComponent
{
    Guid Id { get; init; }
    string ContentType { get; init; }
}
