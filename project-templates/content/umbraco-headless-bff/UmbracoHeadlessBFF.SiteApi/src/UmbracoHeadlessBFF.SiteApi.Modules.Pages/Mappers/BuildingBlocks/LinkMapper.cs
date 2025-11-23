using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.Links;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Configuration;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.BuildingBlocks;

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

    public async Task<Link?> Map(ApiLink? model)
    {
        if (model is null)
        {
            return null;
        }

        UriBuilder uriBuilder;

        switch (model.LinkType)
        {
            case ApiLinkType.Content:
                var link = await _linkService.ResolveLink(model.DestinationId!.Value);

                if (link is null)
                {
                    return null;
                }

                if (_siteResolutionContext.Site.Domains.First().Domain == link.Authority)
                {
                    return new()
                    {
                        Href = link.Path,
                        Title = model.Title,
                        Target = model.Target
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
                    uriBuilder.Path = model.Url;
                }

                break;
            case ApiLinkType.External:
            default:
                if (model.Url?.StartsWith("tel:") is true
                    || model.Url?.StartsWith("mailto:") is true)
                {
                    return new()
                    {
                        Target = null,
                        Href = model.Url,
                        Title = model.Title,
                    };
                }

                if (model.Url?.StartsWith('/') is true)
                {
                    return new()
                    {
                        Target = model.Target,
                        Href = model.Url,
                        Title = model.Title,
                    };
                }

                uriBuilder = new(model.Url!);
                break;
        }

        uriBuilder.Query = model.QueryString ?? uriBuilder.Query;
        uriBuilder.Scheme = "https";

        return new()
        {
            Target = model.Target,
            Href = uriBuilder.Uri.ToString(),
            Title = model.Title,
            IsFile = model.LinkType == ApiLinkType.Media
        };
    }
}
