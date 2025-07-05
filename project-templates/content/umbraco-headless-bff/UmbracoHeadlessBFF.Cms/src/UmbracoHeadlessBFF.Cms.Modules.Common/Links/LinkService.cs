using System.Text.RegularExpressions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using UmbracoHeadlessBFF.Cms.Modules.Common.Umbraco.Models;
using UmbracoHeadlessBFF.SharedModules.Cms.Links;
using UmbracoHeadlessBFF.SharedModules.Common.Strings;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Links;

public sealed partial class LinkService
{
    [GeneratedRegex("^\\d+/(.*)$")]
    private static partial Regex ResolvedRouteRegex();

    private readonly IPublishedContentCache _publishedContentCache;
    private readonly IPublishedUrlProvider _publishedUrlProvider;
    private readonly IDomainCache _domainCache;
    private readonly IVariationContextAccessor _variationContextAccessor;

    public LinkService(IPublishedContentCache publishedContentCache,
        IPublishedUrlProvider publishedUrlProvider,
        IDomainCache domainCache,
        IVariationContextAccessor variationContextAccessor)
    {
        _publishedContentCache = publishedContentCache;
        _publishedUrlProvider = publishedUrlProvider;
        _domainCache = domainCache;
        _variationContextAccessor = variationContextAccessor;
    }

    public Uri? GetUriByContentId(Guid linkId, string culture, bool preview)
    {
        _variationContextAccessor.VariationContext = new(culture);
        var item = _publishedContentCache.GetById(preview, linkId);

        if (item is null)
        {
            return null;
        }

        var home = item.AncestorOrSelf<Home>();

        if (home is null)
        {
            return null;
        }

        var domain = _domainCache.GetAssigned(home.Id).FirstOrDefault(x => x.Culture == culture);

        if (domain is null)
        {
            return null;
        }

        var route = _publishedUrlProvider.GetUrl(item.Id, culture: culture);

        var domainName = domain.Name.Contains("http") ? domain.Name.Replace("http:", "https:") : $"https://{domain.Name}";

        route = ResolvedRouteRegex().Replace(route, domainName.CombineUri("$1"));

        return new(route);
    }

    public Link? GetLinkByContentId(Guid linkId, string culture, bool preview)
    {
        var uri = GetUriByContentId(linkId, culture, preview);

        if (uri is null)
        {
            return null;
        }

        return new()
        {
            Authority = uri.Authority,
            Path = uri.PathAndQuery
        };
    }
}
