using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Media;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages.Abstractions;

public interface IApiSeoSettingsProperties
{
    string? MetaTitle { get; init; }
    string? MetaDescription { get; init; }
    IReadOnlyCollection<ApiMediaWithCrops>? MetaImage { get; init; }
    ApiMediaWithCrops? MetaImageItem => MetaImage?.FirstOrDefault();
    string? OgType { get; init; }
    string? OgDescription { get; init; }
    IReadOnlyCollection<ApiMediaWithCrops>? OgImage { get; init; }
    ApiMediaWithCrops? OgImageItem => OgImage?.FirstOrDefault();
    IReadOnlyCollection<string>? RobotsIndexOptions { get; init; }
    string? RobotsIndexOptionsText => string.Join(' ', RobotsIndexOptions ?? []);
    DateTime? RobotsUnavailableAfter { get; init; }
    bool? SitemapShow { get; init; }
    string? SitemapChangeFrequency { get; init; }
    decimal SitemapPriority { get; init; }
    DateTime? SitemapLastModifiedOverwrite { get; init; }
}

public abstract class ApiSeoSettingsProperties : IApiSeoSettingsProperties
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
