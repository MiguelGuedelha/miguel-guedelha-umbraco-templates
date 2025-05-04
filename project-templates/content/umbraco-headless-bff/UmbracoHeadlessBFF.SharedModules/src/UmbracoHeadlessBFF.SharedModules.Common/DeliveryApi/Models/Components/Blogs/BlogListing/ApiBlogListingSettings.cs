using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Blogs.BlogListing;

public sealed class ApiBlogListingSettings : ApiElement<ApiBlogListingSettingsProperties>
{
    public const string ContentType = "blogListingSettings";
}

public sealed class ApiBlogListingSettingsProperties
{
    public IReadOnlyCollection<IApiContent>? ArticlesContainer { get; init; }
    public IApiContent? ArticlesContainerItem => ArticlesContainer?.FirstOrDefault();
}
