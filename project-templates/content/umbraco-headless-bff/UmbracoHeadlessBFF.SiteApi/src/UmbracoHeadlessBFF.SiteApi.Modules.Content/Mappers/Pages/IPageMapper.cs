using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Pages;

internal interface IPageMapper : IMappingEvaluator, IMapper<IApiContent, IPage>
{
}
