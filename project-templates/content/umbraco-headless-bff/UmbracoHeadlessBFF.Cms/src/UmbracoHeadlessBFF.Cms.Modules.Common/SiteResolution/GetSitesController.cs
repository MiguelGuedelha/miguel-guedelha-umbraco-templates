using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using UmbracoHeadlessBFF.Cms.Modules.Common.Authentication.Attributes;
using UmbracoHeadlessBFF.Cms.Modules.Common.UmbracoModels;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.SiteResolution.Contracts;
using NotFound = Microsoft.AspNetCore.Http.HttpResults.NotFound;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.SiteResolution;

[ApiController]
[Route($"api/v{{version:apiVersion}}/{SiteResolutionConstants.Endpoints.Group}")]
[Tags(SiteResolutionConstants.Endpoints.Tag)]
[ApiVersion(1)]
[ApiKey]
public sealed class GetSitesController : Controller
{
    private readonly IUmbracoContextFactory _umbracoContextFactory;
    private readonly IDomainService _domainService;

    public GetSitesController(IUmbracoContextFactory umbracoContextFactory, IDomainService domainService)
    {
        _umbracoContextFactory = umbracoContextFactory;
        _domainService = domainService;
    }

    [HttpGet("")]
    public Results<Ok<Dictionary<string, SiteDefinition>>, NotFound, ProblemHttpResult> GetSites(bool preview)
    {
        var context = _umbracoContextFactory.EnsureUmbracoContext().UmbracoContext;

        var publishedContentCache = context.Content;

        if (publishedContentCache is null)
        {
            return TypedResults.Problem(new()
            {
                Title = "Content cache doesn't exist",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "The Published Content Cache couldn't be accessed"
            });
        }

        var root = publishedContentCache.GetAtRoot(preview).ToList();

        var homepages = root.SelectMany(x => x.ChildrenForAllCultures).OfType<Home>().Concat(root.OfType<Home>());

        var domainDefinitions = homepages.Select(x => (x, _domainService.GetAssignedDomains(x.Id, false)));

        var siteDefinitions = new Dictionary<string, SiteDefinition>();

        foreach (var (homepage, domains) in domainDefinitions)
        {
            foreach (var domain in domains)
            {
                if (domain.RootContentId is null || domain.LanguageIsoCode is null)
                {
                    continue;
                }

                var siteSettings = homepage.Descendant<SiteSettings>(domain.LanguageIsoCode);
                var dictionary = homepage.Descendant<SiteDictionary>(domain.LanguageIsoCode);
                var notFoundPage = siteSettings?.NotFoundPage;
                var searchPage = siteSettings?.SearchPage;
                var homePageSegment = homepage.UrlSegment(domain.LanguageIsoCode);

                if (siteSettings is null || dictionary is null || homePageSegment is null || notFoundPage is null ||  searchPage is null)
                {
                    continue;
                }

                var siteUri = new Uri(domain.DomainName.StartsWith("http") ? domain.DomainName : $"https://{domain.DomainName}");

                var rootContent = homepage.Root();

                siteDefinitions.TryAdd($"{homepage.Key}-{domain.LanguageIsoCode}", new()
                {
                    RootId = rootContent.Key,
                    SiteSettingsId = siteSettings.Key,
                    NotFoundPageId = notFoundPage.Key,
                    SearchPageId = searchPage.Key,
                    DictionaryId = dictionary.Key,
                    HomepageId = homepage.Key,
                    CultureInfo = domain.LanguageIsoCode,
                    Domain = siteUri.Authority,
                    Path = siteUri.AbsolutePath,
                    BasePath = $"/{homePageSegment}/"
                });
            }
        }

        return siteDefinitions.Count != 0 ? TypedResults.Ok(siteDefinitions) : TypedResults.NotFound();
    }
}
