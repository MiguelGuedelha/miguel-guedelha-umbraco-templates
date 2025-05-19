using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;

internal interface IComponentMapper : IMapper<IApiElement, IApiElement, IComponent>
{
    bool CanMap(string type);
}
