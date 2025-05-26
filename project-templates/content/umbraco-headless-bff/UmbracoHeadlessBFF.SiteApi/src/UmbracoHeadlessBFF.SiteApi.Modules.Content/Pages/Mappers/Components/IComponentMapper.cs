using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.Components;

internal interface IComponentMapper : IMappingEvaluator, IMapper<IApiElement, IApiElement, IComponent>
{
}
