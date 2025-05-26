using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Compositions;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;

public sealed class ApiBlogArticle : IApiContent<ApiBlogArticleProperties>
{
    public Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ContentTypes.ApiBlogArticle;
    public required string Name { get; init; }
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
    public required ApiContentRoute Route { get; init; }
    public Dictionary<string, ApiContentRoute> Cultures { get; init; } = [];
    public required ApiBlogArticleProperties Properties { get; init; }
}

public sealed class ApiBlogArticleProperties : IApiSeoSettingsProperties, IApiListingSettingsProperties, IApiNavigationSettingsProperties, IApiPageContent
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
    public bool ExcludeFromSearch { get; init; }
    public string? ListingTitle { get; init; }
    public string? ListingDescription { get; init; }
    public ApiBlockList<ApiResponsiveImage>? ListingImage { get; init; }
    public bool ShowBreadcrumbs { get; init; }
    public bool ShowInBreadcrumbs { get; init; }
    public string? BreadcrumbNameOverride { get; init; }
    public bool ShowBreadcrumbLink { get; init; }
    public required ApiBlockGrid MainContent { get; init; }
}
