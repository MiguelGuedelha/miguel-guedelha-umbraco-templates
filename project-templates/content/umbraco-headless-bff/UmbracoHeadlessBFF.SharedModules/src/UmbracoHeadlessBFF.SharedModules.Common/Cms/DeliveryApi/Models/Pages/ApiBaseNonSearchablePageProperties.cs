using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;

public abstract class ApiBaseNonSearchablePageProperties : IApiSeoSettingsProperties
{
    public string? MetaTitle { get; init; }
    public string? MetaDescription { get; init; }
    public IReadOnlyCollection<ApiMediaWithCrops>? MetaImage { get; init; }
    public string? OgType { get; init; }
    public string? OgDescription { get; init; }
    public IReadOnlyCollection<ApiMediaWithCrops>? OgImage { get; init; }
    public IReadOnlyCollection<string>? RobotsIndexOptions { get; init; }
    public DateTime? RobotsUnavailableAfter { get; init; }
    public bool? SitemapShow { get; init; }
    public string? SitemapChangeFrequency { get; init; }
    public decimal SitemapPriority { get; init; }
    public DateTime? SitemapLastModifiedOverwrite { get; init; }
}
