namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Components;

internal sealed record FallbackComponent : IComponent
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
#pragma warning disable CA1822
    public bool IsFallbackMapped => true;
#pragma warning restore CA1822
}
