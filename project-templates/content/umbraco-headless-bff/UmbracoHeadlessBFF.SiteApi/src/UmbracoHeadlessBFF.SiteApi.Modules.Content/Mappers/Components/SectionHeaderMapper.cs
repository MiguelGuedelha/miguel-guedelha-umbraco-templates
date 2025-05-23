using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components.Abstractions;

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
