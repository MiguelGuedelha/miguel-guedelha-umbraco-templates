using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Components;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Components;

internal sealed class RichTextSectionMapper : IComponentMapper
{
    private readonly ILinkMapper _linkMapper;
    private readonly IRichTextMapper _richTextMapper;

    public RichTextSectionMapper(ILinkMapper linkMapper, IRichTextMapper richTextMapper)
    {
        _linkMapper = linkMapper;
        _richTextMapper = richTextMapper;
    }

    public bool CanMap(string type) => type == DeliveryApiConstants.ElementTypes.ApiRichTextSection;
    public async Task<IComponent?> Map(IApiElement model, IApiElement? settings)
    {
        if (model is not ApiRichTextSection apiModel)
        {
            return null;
        }

        var link = apiModel.Properties.Cta?.FirstOrDefault();
        var text = apiModel.Properties.Text;

        return new RichTextSection
        {
            Id = apiModel.Id,
            ContentType = apiModel.ContentType,
            Text = text is not null ?  await _richTextMapper.Map(text) : null,
            Cta = link is not null ? await _linkMapper.Map(link) : null
        };
    }
}
