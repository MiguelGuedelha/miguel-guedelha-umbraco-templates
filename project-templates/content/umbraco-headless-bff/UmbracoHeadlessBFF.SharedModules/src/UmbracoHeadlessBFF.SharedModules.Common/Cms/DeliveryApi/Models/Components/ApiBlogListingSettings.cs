using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;

public sealed record ApiBlogListingSettings : IApiElement<ApiBlogListingSettingsProperties>
{
    public required Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ElementTypes.ApiBlogListingSettings;
    public required ApiBlogListingSettingsProperties Properties { get; init; }
}

public sealed record ApiBlogListingSettingsProperties
{
    public IReadOnlyCollection<IApiContent>? ArticlesContainer { get; init; }
}
