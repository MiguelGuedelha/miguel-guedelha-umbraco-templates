using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Links;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Components;

internal sealed class RichTextSectionMapper : IComponentMapper
{
    private readonly IMapper<ApiLink, Link> _linkMapper;

    public RichTextSectionMapper(IMapper<ApiLink, Link> linkMapper)
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
