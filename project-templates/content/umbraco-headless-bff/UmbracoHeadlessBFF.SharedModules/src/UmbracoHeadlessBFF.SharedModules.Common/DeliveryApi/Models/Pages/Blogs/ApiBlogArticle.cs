using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.BlockGrid;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages.Blogs;

public sealed class ApiBlogArticle : ApiContent<ApiBlogArticleProperties>
{
    public const string ContentType = "blogArticle";
}

public sealed class ApiBlogArticleProperties : ApiBaseSearchablePageProperties, IApiPageContent
{
    public required ApiBlockGrid MainContent { get; init; }
}
