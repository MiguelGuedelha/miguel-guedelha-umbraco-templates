using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;

internal sealed class Fallback : Component
{
#pragma warning disable CA1822
    public bool FallbackMapped => true;
#pragma warning restore CA1822
}
