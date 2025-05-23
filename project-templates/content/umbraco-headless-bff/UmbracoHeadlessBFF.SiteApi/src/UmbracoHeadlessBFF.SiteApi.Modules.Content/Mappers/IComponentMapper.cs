using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers;

internal interface IComponentMapper : IMappingEvaluator, IMapper<IApiElement, IApiElement, IComponent>
{
}
