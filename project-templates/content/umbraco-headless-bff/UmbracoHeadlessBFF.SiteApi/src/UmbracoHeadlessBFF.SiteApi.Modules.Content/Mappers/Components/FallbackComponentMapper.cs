using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Components;

internal sealed class FallbackComponentMapper : IComponentMapper
{
    public bool CanMap(string type) => true;

    public Task<IComponent?> Map(IApiElement model, IApiElement? settings)
    {
        return Task.FromResult<IComponent?>(new FallbackComponent
        {
            Id = model.Id,
            ContentType = model.ContentType,
        });
    }
}
