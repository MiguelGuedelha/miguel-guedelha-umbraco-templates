﻿namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Layouts;

internal sealed class FallbackLayout : ILayout
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
#pragma warning disable CA1822
    public bool IsFallbackMapped => true;
#pragma warning restore CA1822
}
