using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Components;

internal interface IComponentMapper : IMappingEvaluator, IMapper<IApiElement, IApiElement, IComponent>
{
}
