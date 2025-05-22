using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.BlockGrid;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Shared;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Blogs;

public sealed class ApiBlogArticle : ApiContent<ApiBlogArticleProperties>
{
}

public sealed class ApiBlogArticleProperties : ApiBaseSearchablePageProperties, IApiPageContent
{
    public required ApiBlockGrid MainContent { get; init; }
}
