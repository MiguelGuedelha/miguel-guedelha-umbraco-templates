﻿using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.Components;

internal sealed class RichTextSectionMapper : IComponentMapper
{
    private readonly ILinkMapper _linkMapper;

    public RichTextSectionMapper(ILinkMapper linkMapper)
    {
        _linkMapper = linkMapper;
    }

    public bool CanMap(string type) => type == DeliveryApiConstants.ElementTypes.ApiRichTextSection;
    public async Task<IComponent?> Map(IApiElement model, IApiElement? settings)
    {
        if (model is not ApiRichTextSection apiModel)
        {
            return null;
        }

        var link = apiModel.Properties.Cta?.FirstOrDefault();

        return new RichTextSection
        {
            Id = apiModel.Id,
            ContentType = apiModel.ContentType,
            Text = apiModel.Properties.Text?.Markup,
            Cta = link is not null ? await _linkMapper.Map(link) : null
        };
    }
}
