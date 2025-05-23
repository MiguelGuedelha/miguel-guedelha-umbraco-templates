using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using UmbracoHeadlessBFF.Cms.Modules.Common.Authentication;
using UmbracoHeadlessBFF.Cms.Modules.Common.UmbracoModels;
using UmbracoHeadlessBFF.SharedModules.Content.Sitemaps;
using NotFound = Microsoft.AspNetCore.Http.HttpResults.NotFound;

namespace UmbracoHeadlessBFF.Cms.Modules.Content.Sitemaps;

[ApiController]
[Route($"api/v{{version:apiVersion}}/{ContentConstants.Endpoints.Group}")]
[Tags(ContentConstants.Endpoints.Tag)]
[ApiVersion(1)]
[ApiKey]
public sealed class GetSitemapController : Controller
{
    private readonly IUmbracoContextFactory _umbracoContextFactory;
    private readonly IVariationContextAccessor _variationContextAccessor;

    public GetSitemapController(IUmbracoContextFactory umbracoContextFactory, IVariationContextAccessor variationContextAccessor)
    {
        _umbracoContextFactory = umbracoContextFactory;
        _variationContextAccessor = variationContextAccessor;
    }

    [HttpGet("sitemap")]
    public Results<Ok<SharedModules.Content.Sitemaps.SitemapData>, NotFound> GetSitemap(Guid siteId, string culture)
    {
        _variationContextAccessor.VariationContext = new(culture);
        var context = _umbracoContextFactory.EnsureUmbracoContext().UmbracoContext;

        var contentCache = context.Content;

        if (contentCache is null)
        {
            return TypedResults.NotFound();
        }

        var siteNode = contentCache.GetById(siteId);

        if (siteNode is null)
        {
            return TypedResults.NotFound();
        }

        var sitemapPages = siteNode
            .DescendantsOrSelf()
            .OfType<ISeoSettings>()
            .Where(x => x.SitemapShow)
            .Select(x => new SitemapItem
            {
                Loc = x.Url(mode: UrlMode.Absolute, culture: culture),
                LastMod = x.SitemapLastModifiedOverwrite != default
                    ? DateOnly.FromDateTime(x.SitemapLastModifiedOverwrite)
                    : DateOnly.FromDateTime(x.UpdateDate),
                ChangeFrequency = x.SitemapChangeFrequency,
                Priority = x.SitemapPriority,
                AlternateLanguages = x.Cultures
                    .Where(nodeCulture => nodeCulture.Value.Culture != culture)
                    .Select(nodeCulture => new SitemapItemAlternateLanguage
                    {
                        HrefLang = nodeCulture.Value.Culture,
                        Href = x.Url(mode: UrlMode.Absolute, culture: nodeCulture.Value.Culture)
                    })
                    .ToArray()
            });


        return TypedResults.Ok(new SharedModules.Content.Sitemaps.SitemapData
        {
            Items = sitemapPages.ToArray()
        });
    }
}
