using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Preview;

[ApiController]
[ApiVersion(1)]
[Route("/api/v{version:apiVersion}/preview")]
[Tags("Preview")]
internal sealed class VerifyPreviewTokenController : ControllerBase
{

    [HttpGet]
    [MapToApiVersion(1)]
    [Route("verify")]
    [Authorize]
    public IActionResult IsAuthorisedToPreview()
    {
        return Ok();
    }
}
