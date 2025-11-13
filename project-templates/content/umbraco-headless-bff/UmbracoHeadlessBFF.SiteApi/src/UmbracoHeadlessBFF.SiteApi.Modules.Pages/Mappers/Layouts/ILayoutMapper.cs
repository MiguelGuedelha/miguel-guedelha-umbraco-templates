using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Layouts;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.Layouts;

internal interface ILayoutMapper : IMappingEvaluator, IMapper<IApiBlockGridItem, ILayout>
{
}
