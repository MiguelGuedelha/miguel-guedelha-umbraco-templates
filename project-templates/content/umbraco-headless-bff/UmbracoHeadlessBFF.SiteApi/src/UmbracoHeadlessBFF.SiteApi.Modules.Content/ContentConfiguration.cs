using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Endpoints;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content;

public static class ContentConfiguration
{
    public static void AddContent(this WebApplicationBuilder builder)
    {

    }

    public static void MapContentEndpoints(this WebApplication app, ApiVersionSet versionSet)
    {
        app.MapGroup("content").WithTags("Content")
            .MapGetContent(versionSet);
    }
}
