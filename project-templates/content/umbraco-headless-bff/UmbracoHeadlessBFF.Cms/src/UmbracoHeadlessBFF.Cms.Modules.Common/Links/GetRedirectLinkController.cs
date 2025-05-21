using System.Net;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using UmbracoHeadlessBFF.Cms.Modules.Common.Authentication.Attributes;

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

    public GetRedirectLinkController(IUmbracoContextFactory umbracoContextFactory,
        IVariationContextAccessor variationContextAccessor, IRedirectUrlService redirectUrlService,
        IPublishedUrlProvider publishedUrlProvider)
    {
        _umbracoContextFactory = umbracoContextFactory;
        _variationContextAccessor = variationContextAccessor;
        _redirectUrlService = redirectUrlService;
        _publishedUrlProvider = publishedUrlProvider;
    }

    [HttpGet("redirects/{path}")]
    public async Task<Results<Ok<string>, NotFound>> GetRedirect(string path, Guid siteId, string culture)
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

        var latestRedirect = await _redirectUrlService.GetMostRecentRedirectUrlAsync($"{siteRoot.Id}/{path.Trim('/')}", culture);

        if (latestRedirect is not null)
        {
            var destination = contentCache.GetById(latestRedirect.ContentId);
            if (destination is not null)
            {
                var url = destination.Url(_publishedUrlProvider, latestRedirect.Culture, UrlMode.Absolute);
                return TypedResults.Ok(url);
            }
        }

        // You can add custom redirects
        // such as Skybrud here, as a fallback after umbraco ones,
        // or replace the above with said custom redirects

        return TypedResults.NotFound();
    }
}
