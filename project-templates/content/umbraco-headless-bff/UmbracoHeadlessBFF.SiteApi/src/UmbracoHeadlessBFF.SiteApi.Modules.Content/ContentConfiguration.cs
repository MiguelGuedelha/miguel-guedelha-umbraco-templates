using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Endpoints;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content;

public static class ContentConfiguration
{
    public static void AddContent(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ContentService>();
    }

    public static void MapContentEndpoints(this RouteGroupBuilder apiVersionGroup)
    {
        apiVersionGroup
            .MapGroup("/content")
            .WithTags("Content")
            .MapGetContent();
    }
}
