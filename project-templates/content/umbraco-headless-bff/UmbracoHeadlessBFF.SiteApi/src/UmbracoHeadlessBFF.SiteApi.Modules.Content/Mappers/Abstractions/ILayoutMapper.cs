using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.BlockGrid;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Layouts.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;

internal interface ILayoutMapper : IMappingEvaluator, IMapper<IApiBlockGridItem, ILayout>
{
}
