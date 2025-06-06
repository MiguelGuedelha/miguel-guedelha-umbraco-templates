﻿using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;

namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages.Compositions;

public interface IApiSeoSettingsProperties
{
    string? MetaTitle { get; init; }
    string? MetaDescription { get; init; }
    IReadOnlyCollection<ApiMediaWithCrops>? MetaImage { get; init; }
    string? OgType { get; init; }
    string? OgDescription { get; init; }
    IReadOnlyCollection<ApiMediaWithCrops>? OgImage { get; init; }
    IReadOnlyCollection<string>? RobotsIndexOptions { get; init; }
    DateTime? RobotsUnavailableAfter { get; init; }
    bool? SitemapShow { get; init; }
    string? SitemapChangeFrequency { get; init; }
    decimal SitemapPriority { get; init; }
    DateTime? SitemapLastModifiedOverwrite { get; init; }
}
