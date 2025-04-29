using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Media;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Pages.Abstractions;

public interface ISeoSettingsPropertiesModel
{
    string? MetaTitle { get; init; }
    string? MetaDescription { get; init; }
    ICollection<ApiMediaWithCropsModel>? MetaImage { get; init; }
    string? OgType { get; init; }
    string? OgDescription { get; init; }
    ICollection<ApiMediaWithCropsModel>? OgImage { get; init; }
    IReadOnlyCollection<string>? RobotsIndexOptions { get; init; }
    DateTime? RobotsUnavailableAfter { get; init; }
    bool? SitemapShow { get; init; }
    string? SitemapChangeFrequency { get; init; }
    decimal? SitemapPriority { get; init; }
    DateTime? SitemapLastModifiedOverwrite { get; init; }
}
