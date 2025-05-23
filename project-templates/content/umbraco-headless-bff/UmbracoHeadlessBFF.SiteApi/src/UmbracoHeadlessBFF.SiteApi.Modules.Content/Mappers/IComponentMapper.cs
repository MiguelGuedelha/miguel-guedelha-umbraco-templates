using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;

internal interface IComponentMapper : IMappingEvaluator, IMapper<IApiElement, IApiElement, IComponent>
{
}
