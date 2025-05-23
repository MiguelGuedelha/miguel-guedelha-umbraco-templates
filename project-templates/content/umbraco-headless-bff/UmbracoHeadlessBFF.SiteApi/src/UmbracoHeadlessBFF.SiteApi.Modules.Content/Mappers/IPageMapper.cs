using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;

internal interface IPageMapper : IMappingEvaluator, IMapper<IApiContent, IPage>
{
}
