﻿namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.BuildingBlocks;

internal sealed record SocialLink
{
    public string? Network { get; init; }
    public string? Name { get; init; }
    public string? Url { get; init; }
}
