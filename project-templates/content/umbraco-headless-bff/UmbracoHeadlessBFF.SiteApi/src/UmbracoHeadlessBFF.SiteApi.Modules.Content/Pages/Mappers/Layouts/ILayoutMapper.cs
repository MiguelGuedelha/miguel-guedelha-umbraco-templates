using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Layouts;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.Layouts;

internal interface ILayoutMapper : IMappingEvaluator, IMapper<IApiBlockGridItem, ILayout>
{
}
