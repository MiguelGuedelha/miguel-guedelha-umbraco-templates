using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.Components;

internal interface IComponentMapper : IMappingEvaluator, IMapper<IApiElement, IApiElement, IComponent>
{
}
