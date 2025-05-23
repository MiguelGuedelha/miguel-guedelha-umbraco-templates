using System.Diagnostics;
using System.Net;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using UmbracoHeadlessBFF.Cms.Modules.Common.Authentication;
using UmbracoHeadlessBFF.Cms.Modules.Common.UmbracoModels;
using UmbracoHeadlessBFF.Cms.Modules.Common.Urls;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.Links;
using NotFound = Microsoft.AspNetCore.Http.HttpResults.NotFound;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Links;

[ApiController]
[Route($"api/v{{version:apiVersion}}/{LinksConstants.Endpoints.Group}")]
[Tags(LinksConstants.Endpoints.Tag)]
[ApiVersion(1)]
[ApiKey]
public sealed class GetRedirectLinkController : Controller
{
    private readonly IUmbracoContextFactory _umbracoContextFactory;
    private readonly IVariationContextAccessor _variationContextAccessor;
    private readonly IRedirectUrlService _redirectUrlService;
    private readonly IPublishedUrlProvider _publishedUrlProvider;
    private readonly ApplicationUrlOptions _applicationUrlOptions;

    private static readonly HashSet<string> s_nonValidDestinationPageTypes = [
        SiteSettings.ModelTypeAlias,
        SiteDictionary.ModelTypeAlias,
        SiteGrouping.ModelTypeAlias,
        UmbracoModels.NotFound.ModelTypeAlias,
    ];

    public GetRedirectLinkController(IUmbracoContextFactory umbracoContextFactory,
        IVariationContextAccessor variationContextAccessor, IRedirectUrlService redirectUrlService,
        IPublishedUrlProvider publishedUrlProvider, IOptionsSnapshot<ApplicationUrlOptions> applicationUrlOptions)
    {
        _umbracoContextFactory = umbracoContextFactory;
        _variationContextAccessor = variationContextAccessor;
        _redirectUrlService = redirectUrlService;
        _publishedUrlProvider = publishedUrlProvider;
        _applicationUrlOptions = applicationUrlOptions.Value;
    }

    [HttpGet("redirects/{path}")]
    public async Task<Results<Ok<RedirectLink>, NotFound>> GetRedirect(string path, Guid siteId, string culture)
    {
        _variationContextAccessor.VariationContext = new(culture);

        using var context = _umbracoContextFactory.EnsureUmbracoContext().UmbracoContext;

        var contentCache = context.Content;

        if (contentCache is null)
        {
            throw new InvalidOperationException("No content cache");
        }

        if (path.Contains("%2F", StringComparison.OrdinalIgnoreCase))
        {
            path = WebUtility.UrlDecode(path);
        }

        var siteRoot = contentCache.GetById(siteId);

        if (siteRoot is null)
        {
            return TypedResults.NotFound();
        }

        var itemRoute = $"{siteRoot.Id}/{path.Trim('/')}";

        var latestRedirect = await _redirectUrlService.GetMostRecentRedirectUrlAsync(itemRoute, culture);

        if (latestRedirect is not null)
        {
            var destination = contentCache.GetById(latestRedirect.ContentId);
            if (destination is not null)
            {
                var destinationUrl = destination.Url(_publishedUrlProvider, latestRedirect.Culture, UrlMode.Absolute);
                return TypedResults.Ok(new RedirectLink
                {
                    Location = destinationUrl,
                    StatusCode = StatusCodes.Status302Found
                });
            }
        }

        // You can add custom redirects
        // such as Skybrud here, as a fallback after umbraco ones,
        // or replace the above with said custom redirects


        // Handles all the pages which have the Redirect Settings composition and are not meant to be displayed on the site
        var item = contentCache.GetByRoute(itemRoute);

        if (item is not IRedirectSettings redirectSettings)
        {
            return TypedResults.NotFound();
        }

        var url = redirectSettings.RedirectLink switch
        {
            { Type: LinkType.Content } => redirectSettings.RedirectLink.Content?.Url(_publishedUrlProvider, culture, UrlMode.Absolute),
            { Type: LinkType.Media } => new Uri(new(_applicationUrlOptions.Media), redirectSettings.RedirectLink.Url).ToString(),
            { Type: LinkType.External } => redirectSettings.RedirectLink.Url!,
            _ => GenerateFallbackUrl(item, redirectSettings.RedirectDirection, culture)
        };

        if (url is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new RedirectLink
        {
            Location = url,
            StatusCode = StatusCodes.Status302Found
        });
    }

    private string? GenerateFallbackUrl(IPublishedContent item, RedirectFallbackDirection redirectDirection, string culture)
    {
        var possibleItems = redirectDirection switch
        {
            RedirectFallbackDirection.Ancestor => item.Ancestors(),
            RedirectFallbackDirection.Descendant => item.Descendants(),
            RedirectFallbackDirection.Sibling => item.Siblings(),
            _ => throw new UnreachableException("Shouldn't happen, non existent redirect direction")
        };

        var destination = possibleItems
            ?.FirstOrDefault(x => !s_nonValidDestinationPageTypes.Contains(x.ContentType.Alias) && x is not IRedirectSettings);

        return destination?.Url(_publishedUrlProvider, culture, UrlMode.Absolute);
    }
}
