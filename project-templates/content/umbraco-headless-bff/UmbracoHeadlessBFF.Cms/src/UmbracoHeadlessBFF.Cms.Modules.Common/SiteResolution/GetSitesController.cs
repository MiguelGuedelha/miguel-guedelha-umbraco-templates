using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using UmbracoHeadlessBFF.Cms.Modules.Common.Authentication;
using UmbracoHeadlessBFF.Cms.Modules.Common.UmbracoModels;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.SiteResolution;
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
        using var context = _umbracoContextFactory.EnsureUmbracoContext();

        var publishedContentCache = context.UmbracoContext.Content;

        if (publishedContentCache is null)
        {
            return TypedResults.Problem(new ProblemDetails
            {
                Title = "Content cache doesn't exist",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "The Published Content Cache couldn't be accessed"
            });
        }

        var root = publishedContentCache.GetAtRoot(preview).ToArray();

        var homepages = root
            .OfType<SiteGrouping>()
            .SelectMany(x => x.ChildrenForAllCultures)
            .OfType<Home>()
            .Concat(root.OfType<Home>());

        var domainDefinitions = homepages.Select(x => (x, _domainService.GetAssignedDomains(x.Id, false)));

        var siteDefinitions = new Dictionary<string, SiteDefinition>();

        foreach (var (homepage, domains) in domainDefinitions)
        {
            var domainsPerLanguage = domains.GroupBy(x => x.LanguageIsoCode);

            foreach (var domainGroup in domainsPerLanguage)
            {
                var firstDomain = domainGroup.FirstOrDefault();

                if (firstDomain is null || domainGroup.Key is null)
                {
                    continue;
                }

                if (firstDomain.RootContentId is null || firstDomain.LanguageIsoCode is null)
                {
                    continue;
                }

                var siteSettings = homepage.Descendant<SiteSettings>(firstDomain.LanguageIsoCode);
                var dictionary = homepage.Descendant<SiteDictionary>(firstDomain.LanguageIsoCode);
                var homePageSegment = homepage.UrlSegment(firstDomain.LanguageIsoCode);
                var notFoundPage = siteSettings?.NotFoundPage;
                var searchPage = siteSettings?.SearchPage;

                if (siteSettings is null || dictionary is null || homePageSegment is null || searchPage is null || notFoundPage is null)
                {
                    continue;
                }

                var rootContent = homepage.Root();

                var siteDefinition = new SiteDefinition
                {
                    RootId = rootContent.Key,
                    SiteSettingsId = siteSettings.Key,
                    NotFoundPageId = notFoundPage.Key,
                    SearchPageId = searchPage.Key,
                    DictionaryId = dictionary.Key,
                    HomepageId = homepage.Key,
                    CultureInfo = domainGroup.Key,
                    BasePath = $"/{homePageSegment}/"
                };

                var siteDomains = domainGroup
                    .Select(domain => new Uri(domain.DomainName.StartsWith("http") ? domain.DomainName : $"https://{domain.DomainName}"))
                    .Select(siteUri => new SiteDefinitionDomain { Scheme = siteUri.Scheme, Domain = siteUri.Authority, Path = $"/{siteUri.AbsolutePath.Trim('/')}/" })
                    .ToArray();

                siteDefinition = siteDefinition with { Domains = siteDomains };

                siteDefinitions.Add($"{homepage.Key}-{domainGroup.Key}", siteDefinition);
            }
        }

        return siteDefinitions.Count != 0 ? TypedResults.Ok(siteDefinitions) : TypedResults.NotFound();
    }
}
