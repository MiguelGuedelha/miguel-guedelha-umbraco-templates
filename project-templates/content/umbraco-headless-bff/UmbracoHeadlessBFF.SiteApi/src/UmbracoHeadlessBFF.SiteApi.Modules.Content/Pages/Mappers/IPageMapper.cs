using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Pages;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers;

internal interface IPageMapper : IMappingEvaluator, IMapper<IApiContent, IPage>
{
}
