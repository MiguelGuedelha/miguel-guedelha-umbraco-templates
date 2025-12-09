using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Components;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.Components;

internal sealed class BlogListingMapper : IComponentMapper
{
    public BlogListingMapper()
    {
    }

    public bool CanMap(string type) => type == DeliveryApiConstants.ElementTypes.ApiBlogListing;

    public Task<IComponent?> Map(IApiElement? model, IApiElement? settings)
    {
        if (model is not ApiBlogListing apiModel || settings is not ApiBlogListingSettings apiSettings)
        {
            return Task.FromResult<IComponent?>(null);
        }

        var pagedAmount = apiSettings.Properties.PagedAmount ?? 9;

        pagedAmount = pagedAmount <= 0 ? 9 : pagedAmount;

        return Task.FromResult<IComponent?>(new BlogListing
        {
            Id = apiModel.Id,
            ContentType = apiModel.ContentType,
            Heading = apiModel.Properties.Heading,
            HeadingSize = apiModel.Properties.HeadingSize,
            SubHeading = apiModel.Properties.SubHeading,
            AncestorId = apiSettings.Properties.ArticlesContainer?.FirstOrDefault()?.Id,
            PagedAmount = pagedAmount,
            Sorting = apiSettings.Properties.Sorting ?? "latest"
        });
    }
}
