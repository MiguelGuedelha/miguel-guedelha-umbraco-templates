using System.Text.RegularExpressions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Links;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.Links;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;

internal sealed partial class LinkMapper : IMapper<ApiLink, Link>
{
    private readonly SiteResolutionContext _siteResolutionContext;
    private readonly LinkService _linkService;

    [GeneratedRegex(@"^https?://([a-zA-Z0-9-\.]+):?(\d+)?$")]
    public static partial Regex BaseAddressRegex();

    public LinkMapper(SiteResolutionContext siteResolutionContext, LinkService linkService)
    {
        _siteResolutionContext = siteResolutionContext;
        _linkService = linkService;
    }

    public async Task<Link> Map(ApiLink apiModel)
    {
        UriBuilder uriBuilder;

        switch (apiModel.LinkType)
        {
            case ApiLinkType.Content:
                var link = await _linkService.ResolveLink(apiModel.DestinationId!.Value, _siteResolutionContext.Site.CultureInfo);

                if (_siteResolutionContext.Site.Domains.First().Domain == link.Authority)
                {
                    return new Link
                    {
                        Href = link.Path,
                        Title = apiModel.Title,
                        Target = apiModel.Target
                    };
                }

                var hostPortSplit = link.Authority.Split(":");

                uriBuilder = new UriBuilder(hostPortSplit[0])
                {
                    Path = link.Path,
                    Port = hostPortSplit.Length > 1 ? int.Parse(hostPortSplit[1]) : -1
                };

                break;

            case ApiLinkType.Media:
            case ApiLinkType.External:
            default:
            {
                if (apiModel.Url?.StartsWith("tel:") is true || apiModel.Url?.StartsWith("mailto:") is true)
                {
                    return new Link
                    {
                        Target = null,
                        Href = apiModel.Url,
                        Title = apiModel.Title,
                    };
                }

                uriBuilder = new UriBuilder(apiModel.Url!);
                break;
            }
        }

        uriBuilder.Query = apiModel.QueryString ?? uriBuilder.Query;
        uriBuilder.Scheme = "https";

        return new Link
        {
            Target = apiModel.Target,
            Href = uriBuilder.Uri.ToString(),
            Title = apiModel.Title,
            IsFile = apiModel.LinkType == ApiLinkType.Media
        };
    }
}
