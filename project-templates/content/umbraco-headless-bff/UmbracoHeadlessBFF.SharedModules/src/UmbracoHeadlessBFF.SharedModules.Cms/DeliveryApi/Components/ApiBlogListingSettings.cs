using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages;

namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Components;

public sealed record ApiBlogListingSettings : IApiElement<ApiBlogListingSettingsProperties>
{
    public required Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ElementTypes.ApiBlogListingSettings;
    public required ApiBlogListingSettingsProperties Properties { get; init; }
}

public sealed record ApiBlogListingSettingsProperties
{
    public IReadOnlyCollection<ApiContentReference>? ArticlesContainer { get; init; }
    public int? PagedAmount { get; init; }
    public string? Sorting { get; init; }
}
