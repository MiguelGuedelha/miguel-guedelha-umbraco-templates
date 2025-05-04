using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Blogs.BlogListing;

public sealed class ApiBlogListingSettings : ApiElement<ApiBlogListingSettingsProperties>
{
    public const string ContentType = "blogListingSettings";
}

public sealed class ApiBlogListingSettingsProperties
{
    public IReadOnlyCollection<IApiContent>? ArticlesContainer { get; init; }
    public IApiContent? ArticlesContainerItem => ArticlesContainer?.FirstOrDefault();
}
