using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.Links;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Urls;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;

internal interface ILinkMapper : IMapper<ApiLink, Link>
{
}

internal sealed partial class LinkMapper : ILinkMapper
{
    private readonly SiteResolutionContext _siteResolutionContext;
    private readonly LinkService _linkService;
    private readonly ApplicationUrlOptions _applicationUrlOptions;

    public LinkMapper(SiteResolutionContext siteResolutionContext, LinkService linkService, IOptionsSnapshot<ApplicationUrlOptions> applicationUrlOptions)
    {
        _siteResolutionContext = siteResolutionContext;
        _linkService = linkService;
        _applicationUrlOptions = applicationUrlOptions.Value;
    }

    [GeneratedRegex(@"^https?://([a-zA-Z0-9-\.]+):?(\d+)?$")]
    private static partial Regex DomainRegex();

    public async Task<Link?> Map(ApiLink apiModel)
    {
        UriBuilder uriBuilder;

        switch (apiModel.LinkType)
        {
            case ApiLinkType.Content:
                var link = await _linkService.ResolveLink(apiModel.DestinationId!.Value);

                if (link is null)
                {
                    return null;
                }

                if (_siteResolutionContext.Site.Domains.First().Domain == link.Authority)
                {
                    return new()
                    {
                        Href = link.Path,
                        Title = apiModel.Title,
                        Target = apiModel.Target
                    };
                }

                var hostPortSplit = link.Authority.Split(":");

                uriBuilder = new(hostPortSplit[0])
                {
                    Path = link.Path,
                    Port = hostPortSplit.Length > 1 ? int.Parse(hostPortSplit[1]) : -1
                };

                break;

            case ApiLinkType.Media:
                var match = DomainRegex().Match(_applicationUrlOptions.Media);

                if (!match.Success || match.Groups.Count < 2)
                {
                    return null;
                }

                uriBuilder = new() { Host = match.Groups[1].Value };

                if (match.Groups.Count == 3 && int.TryParse(match.Groups[2].Value, out var port))
                {
                    uriBuilder.Port = port;
                    uriBuilder.Path = apiModel.Url;
                }

                break;
            case ApiLinkType.External:
            default:
                if (apiModel.Url?.StartsWith("tel:") is true || apiModel.Url?.StartsWith("mailto:") is true)
                {
                    return new()
                    {
                        Target = null,
                        Href = apiModel.Url,
                        Title = apiModel.Title,
                    };
                }

                uriBuilder = new(apiModel.Url!);
                break;
        }

        uriBuilder.Query = apiModel.QueryString ?? uriBuilder.Query;
        uriBuilder.Scheme = "https";

        return new()
        {
            Target = apiModel.Target,
            Href = uriBuilder.Uri.ToString(),
            Title = apiModel.Title,
            IsFile = apiModel.LinkType == ApiLinkType.Media
        };
    }
}
