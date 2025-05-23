﻿using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UmbracoHeadlessBFF.Cms.Modules.Common.Authentication;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.Links;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Links;

[ApiController]
[Route($"api/v{{version:apiVersion}}/{LinksConstants.Endpoints.Group}")]
[Tags(LinksConstants.Endpoints.Tag)]
[ApiVersion(1)]
[ApiKey]
public sealed class GetLinkByIdController
{
    private readonly LinkService _linkService;

    public GetLinkByIdController(LinkService linkService)
    {
        _linkService = linkService;
    }

    [HttpGet("{id:guid}")]
    public Results<Ok<Link>, NotFound, ProblemHttpResult> GetLinkById(Guid id, string culture, bool preview)
    {
        var link = _linkService.GetLinkByContentId(id, culture, preview);

        return link is not null ? TypedResults.Ok(link) : TypedResults.NotFound();
    }
}
