using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks.Media;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Components;

internal sealed class FullWidthImageMapper : IComponentMapper
{
    private readonly IResponsiveImageMapper _responsiveImageMapper;

    public FullWidthImageMapper(IResponsiveImageMapper responsiveImageMapper)
    {
        _responsiveImageMapper = responsiveImageMapper;
    }

    public bool CanMap(string type) => type == DeliveryApiConstants.ElementTypes.ApiFullWidthImage;

    public async Task<IComponent?> Map(IApiElement model, IApiElement? settings)
    {
        if (model is not ApiFullWidthImage apiModel)
        {
            return null;
        }

        var image = apiModel.Properties.Image?.Items.FirstOrDefault()?.Content;

        return new FullWidthImage
        {
            Id = apiModel.Id,
            ContentType = apiModel.ContentType,
            Image = image is not null ? await _responsiveImageMapper.Map(image) : null
        };
    }
}
