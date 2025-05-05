using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.GetContent;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content;

public static class ContentConfiguration
{
    public static void AddContent(this WebApplicationBuilder builder)
    {

    }

    public static void MapContentEndpoints(this WebApplication app, ApiVersionSet versionSet)
    {
        app.MapGroup("content")
            .MapGetContent(versionSet);
    }
}
