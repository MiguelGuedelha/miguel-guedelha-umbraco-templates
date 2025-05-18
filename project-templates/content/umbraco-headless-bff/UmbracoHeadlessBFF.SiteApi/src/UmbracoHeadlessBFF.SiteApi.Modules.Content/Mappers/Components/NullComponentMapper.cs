using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Components;

internal sealed class NullComponentMapper : IComponentMapper
{
    public bool CanMap(string type)
    {
        return true;
    }

    public Task<IComponent?> Map(IApiElement model)
    {
        return Task.FromResult<IComponent?>(null);
    }
}
