using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Media;

namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Pages.Abstractions;

public interface IApiSeoSettingsProperties
{
    string? MetaTitle { get; init; }
    string? MetaDescription { get; init; }
    ICollection<ApiMediaWithCrops>? MetaImage { get; init; }
    string? OgType { get; init; }
    string? OgDescription { get; init; }
    ICollection<ApiMediaWithCrops>? OgImage { get; init; }
    IReadOnlyCollection<string>? RobotsIndexOptions { get; init; }
    DateTime? RobotsUnavailableAfter { get; init; }
    bool? SitemapShow { get; init; }
    string? SitemapChangeFrequency { get; init; }
    decimal? SitemapPriority { get; init; }
    DateTime? SitemapLastModifiedOverwrite { get; init; }
}
