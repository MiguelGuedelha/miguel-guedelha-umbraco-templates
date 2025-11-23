using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Layouts;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.Components;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Components;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Layouts;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.Layouts;

internal sealed class OneColumnMapper : ILayoutMapper
{
    private readonly IEnumerable<IComponentMapper> _componentMappers;

    public OneColumnMapper(IEnumerable<IComponentMapper> componentMappers)
    {
        _componentMappers = componentMappers;
    }

    public bool CanMap(string type) => type == DeliveryApiConstants.ElementTypes.ApiOneColumn;

    public async Task<ILayout?> Map(IApiBlockGridItem? model)
    {
        if (model?.Content is not ApiOneColumn)
        {
            return null;
        }

        var componentMappingTasks = model.Areas
            .FirstOrDefault()?
            .Items
            .Select(item => _componentMappers
                .First(mapper => mapper.CanMap(item.Content.ContentType))
                .Map(item.Content, item.Settings))
            .ToArray() ?? [];

        await Task.WhenAll(componentMappingTasks);

        return new OneColumn
        {
            Id = model.Content.Id,
            ContentType = model.Content.ContentType,
            Single = componentMappingTasks.Select(x => x.Result).OfType<IComponent>().ToArray()
        };
    }
}
