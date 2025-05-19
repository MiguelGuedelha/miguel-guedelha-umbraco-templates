using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Layouts.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Layouts;

internal sealed class FallbackLayout : Layout
{
#pragma warning disable CA1822
    public bool IsFallbackMapped => true;
#pragma warning restore CA1822
}
