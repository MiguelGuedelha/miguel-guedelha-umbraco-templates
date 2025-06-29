using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Services.Navigation;
using Umbraco.Extensions;
using UmbracoHeadlessBFF.Cms.Modules.Common.Authentication;
using UmbracoHeadlessBFF.Cms.Modules.Common.Umbraco.Models;
using UmbracoHeadlessBFF.SharedModules.Cms.SiteResolution;
using UmbracoHeadlessBFF.SharedModules.Common.Strings;
using NotFound = Microsoft.AspNetCore.Http.HttpResults.NotFound;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.SiteResolution;

[ApiController]
[Route($"api/v{{version:apiVersion}}/{SiteResolutionConstants.Endpoints.Group}")]
[Tags(SiteResolutionConstants.Endpoints.Tag)]
[ApiVersion(1)]
[ApiKey]
public sealed class GetSitesController : Controller
{
    private readonly IDomainService _domainService;
    private readonly IPublishedContentCache _publishedContentCache;
    private readonly IDocumentNavigationQueryService _documentNavigationQueryService;

    public GetSitesController(IDomainService domainService,
        IPublishedContentCache publishedContentCache,
        IDocumentNavigationQueryService documentNavigationQueryService)
    {
        _domainService = domainService;
        _publishedContentCache = publishedContentCache;
        _documentNavigationQueryService = documentNavigationQueryService;
    }

    [HttpGet("")]
    public async Task<Results<Ok<Dictionary<string, SiteDefinition>>, NotFound, ProblemHttpResult>> GetSites(bool preview)
    {
        _documentNavigationQueryService.TryGetRootKeys(out var keys);

        var rootTasks = keys.Select(x => _publishedContentCache.GetByIdAsync(x, preview)).ToArray();

        await Task.WhenAll(rootTasks);

        var root = rootTasks.Select(x => x.Result).ToArray();

        var homepages = root.OfType<Home>();

        var homepagesUnderGroupsTasks = root
            .OfType<SiteGrouping>()
            .SelectMany(x =>
            {
                _documentNavigationQueryService.TryGetChildrenKeysOfType(x.Key, Home.ModelTypeAlias, out var homeIds);

                return homeIds;
            })
            .Select(x => _publishedContentCache.GetByIdAsync(x, preview))
            .ToArray();

        await Task.WhenAll(homepagesUnderGroupsTasks);

        homepages = homepages
            .Concat(homepagesUnderGroupsTasks
                .Select(x => x.Result)
                .OfType<Home>())
            .ToArray();

        var domainTasks = homepages.Select(x => _domainService.GetAssignedDomainsAsync(x.Key, false)).ToArray();

        await Task.WhenAll(domainTasks);

        var domainDefinitions = homepages.Zip(domainTasks.Select(x => x.Result));

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
                    .Select(siteUri => new SiteDefinitionDomain { Scheme = siteUri.Scheme, Domain = siteUri.Authority, Path = siteUri.AbsolutePath.SanitisePathSlashes() })
                    .ToArray();

                siteDefinition = siteDefinition with { Domains = siteDomains };

                siteDefinitions.Add($"{homepage.Key}-{domainGroup.Key}", siteDefinition);
            }
        }

        return siteDefinitions.Count != 0 ? TypedResults.Ok(siteDefinitions) : TypedResults.NotFound();
    }
}
