using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks.Media;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Components;

internal sealed class FullWidthImageMapper : IComponentMapper
{
    private readonly IMapper<ApiResponsiveImage, ResponsiveImage> _responsiveImageMapper;

    public FullWidthImageMapper(IMapper<ApiResponsiveImage, ResponsiveImage> responsiveImageMapper)
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
