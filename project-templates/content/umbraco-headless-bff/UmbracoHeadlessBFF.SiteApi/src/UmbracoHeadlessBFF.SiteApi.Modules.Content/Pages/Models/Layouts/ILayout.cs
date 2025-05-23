namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Layouts;

internal interface ILayout
{
    Guid Id { get; init; }
    string ContentType { get; init; }
}
