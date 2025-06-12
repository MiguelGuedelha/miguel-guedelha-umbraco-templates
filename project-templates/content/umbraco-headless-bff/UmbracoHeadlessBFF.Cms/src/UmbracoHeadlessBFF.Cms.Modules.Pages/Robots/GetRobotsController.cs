﻿using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using UmbracoHeadlessBFF.Cms.Modules.Common.Authentication;
using UmbracoHeadlessBFF.Cms.Modules.Common.Umbraco.Models;
using UmbracoHeadlessBFF.SharedModules.Cms.Robots;
using NotFound = Microsoft.AspNetCore.Http.HttpResults.NotFound;

namespace UmbracoHeadlessBFF.Cms.Modules.Pages.Robots;

[ApiController]
[Route($"api/v{{version:apiVersion}}/{PagesConstants.Endpoints.Group}")]
[Tags(PagesConstants.Endpoints.Tag)]
[ApiVersion(1)]
[ApiKey]
public sealed class GetRobotsController : Controller
{
    private readonly IUmbracoContextFactory _umbracoContextFactory;
    private readonly IVariationContextAccessor _variationContextAccessor;

    public GetRobotsController(
        IUmbracoContextFactory umbracoContextFactory,
        IVariationContextAccessor variationContextAccessor)
    {
        _umbracoContextFactory = umbracoContextFactory;
        _variationContextAccessor = variationContextAccessor;
    }


    [HttpGet("robots")]
    public Results<Ok<RobotsData>, NotFound> GetRobotsTxt(Guid siteId, string culture, bool preview)
    {
        _variationContextAccessor.VariationContext = new(culture);
        var context = _umbracoContextFactory.EnsureUmbracoContext().UmbracoContext;

        var contentCache = context.Content;

        if (contentCache is null)
        {
            return TypedResults.NotFound();
        }

        var siteNode = contentCache.GetById(preview, siteId);

        if (siteNode is null)
        {
            return TypedResults.NotFound();
        }

        var siteSettings = siteNode.FirstChild<SiteSettings>(culture);

        return siteSettings switch
        {
            { RobotsTxtFileContent: not null } =>
                TypedResults.Ok(new RobotsData
                {
                    RobotsContent = siteSettings.RobotsTxtFileContent
                }),
            _ => TypedResults.NotFound()
        };
    }
}
