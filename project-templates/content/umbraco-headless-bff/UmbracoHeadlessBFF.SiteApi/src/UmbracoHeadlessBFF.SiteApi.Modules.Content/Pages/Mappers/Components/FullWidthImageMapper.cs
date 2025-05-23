﻿using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.Components;

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
