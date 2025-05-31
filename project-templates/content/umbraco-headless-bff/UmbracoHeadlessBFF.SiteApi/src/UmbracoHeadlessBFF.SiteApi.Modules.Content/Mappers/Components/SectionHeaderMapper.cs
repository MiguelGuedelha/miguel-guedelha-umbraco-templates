using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Components;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Components;

internal sealed class SectionHeaderMapper : IComponentMapper
{
    public bool CanMap(string type) => type == DeliveryApiConstants.ElementTypes.ApiSectionHeader;
    public Task<IComponent?> Map(IApiElement model, IApiElement? settings)
    {
        if (model is not ApiSectionHeader apiModel)
        {
            return Task.FromResult(default(IComponent));
        }

        return Task.FromResult<IComponent?>(new SectionHeader
        {
            Id = apiModel.Id,
            ContentType = apiModel.ContentType,
            Heading = apiModel.Properties.Heading,
            HeadingSize = apiModel.Properties.HeadingSize,
            SubHeading = apiModel.Properties.SubHeading,
        });
    }
}
