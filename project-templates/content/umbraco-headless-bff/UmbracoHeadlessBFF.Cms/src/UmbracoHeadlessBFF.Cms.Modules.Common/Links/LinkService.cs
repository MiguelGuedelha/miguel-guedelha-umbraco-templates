﻿using System.Text.RegularExpressions;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using UmbracoHeadlessBFF.Cms.Modules.Common.UmbracoModels;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.Links;
using UmbracoHeadlessBFF.SharedModules.Common.Strings;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Links;

public sealed partial class LinkService
{
    [GeneratedRegex("^\\d+/(.*)$")]
    private static partial Regex ResolvedRouteRegex();

    private readonly IUmbracoContextFactory _umbracoContextFactory;

    public LinkService(IUmbracoContextFactory umbracoContextFactory)
    {
        _umbracoContextFactory = umbracoContextFactory;
    }

    public Link? GetLinkByContentId(Guid linkId, string culture, bool preview)
    {
        using var context = _umbracoContextFactory.EnsureUmbracoContext();

        var contentCache = context.UmbracoContext.Content;

        var item = contentCache?.GetById(preview, linkId);

        if (item is null)
        {
            return null;
        }

        var home = item.AncestorOrSelf<Home>();

        if (home is null)
        {
            return null;
        }

        var domainCache = context.UmbracoContext.Domains;

        var domain = domainCache?.GetAssigned(home.Id).FirstOrDefault(x => x.Culture == culture);

        if (domain is null)
        {
            return null;
        }

        var route = contentCache?.GetRouteById(preview, item.Id, culture);

        if (route is null)
        {
            return null;
        }

        var domainName = domain.Name.Contains("http") ? domain.Name.Replace("http:", "https:") : $"https://{domain.Name}";

        route = ResolvedRouteRegex().Replace(route, domainName.CombineUri("$1"));

        var uri = new Uri(route);

        return new() { Authority = uri.Authority, Path = uri.PathAndQuery };
    }
}
