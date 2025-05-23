namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Components;

internal sealed class FallbackComponent : IComponent
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
#pragma warning disable CA1822
    public bool IsFallbackMapped => true;
#pragma warning restore CA1822
}
