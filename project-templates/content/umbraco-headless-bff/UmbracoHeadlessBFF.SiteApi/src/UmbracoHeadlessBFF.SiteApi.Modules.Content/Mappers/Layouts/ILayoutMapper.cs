using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Layouts;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Layouts;

internal interface ILayoutMapper : IMappingEvaluator, IMapper<IApiBlockGridItem, ILayout>
{
}
