namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Components;

internal interface IComponent
{
    Guid Id { get; init; }
    string ContentType { get; init; }
}
