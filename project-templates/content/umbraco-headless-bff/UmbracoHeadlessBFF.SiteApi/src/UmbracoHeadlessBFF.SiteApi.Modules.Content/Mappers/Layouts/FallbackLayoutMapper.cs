using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.BlockGrid;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Layouts;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Layouts.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Layouts;

internal sealed class FallbackLayoutMapper : ILayoutMapper
{
    public bool CanMap(string type) => true;

    public Task<ILayout?> Map(IApiBlockGridItem model)
    {
        return Task.FromResult<ILayout?>(new FallbackLayout
        {
            Id = model.Content.Id,
            ContentType = model.Content.ContentType,
        });
    }
}
