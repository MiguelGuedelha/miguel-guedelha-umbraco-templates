using Scalar.AspNetCore;
using Umbraco.Cms.Api.Common.DependencyInjection;
using Umbraco.Cms.Core;
using UmbracoHeadlessBFF.Cms.Modules.Caching;
using UmbracoHeadlessBFF.Cms.Modules.Common.Authentication;
using UmbracoHeadlessBFF.Cms.Modules.Common.Links;
using UmbracoHeadlessBFF.Cms.Modules.Common.Preview;
using UmbracoHeadlessBFF.Cms.Modules.Common.Umbraco;
using UmbracoHeadlessBFF.Cms.Modules.Common.Urls;
using UmbracoHeadlessBFF.SharedModules.Common.Caching;
using UmbracoHeadlessBFF.SharedModules.Common.Correlation;

var builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .AddAzureBlobMediaFileSystem()
    .AddAzureBlobImageSharpCache()
    .Build();

builder.AddServiceDefaults();

builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<AuthenticationSwaggerParameters>();
});

builder.AddCorrelationCommonSharedModule();
builder.AddPreviewModule();
builder.AddLinksModule();
builder.AddUrlsModule();
builder.AddUmbracoOverrides();
builder.AddCachingModule();

builder.Services.AddControllers().AddJsonOptions(Constants.JsonOptionsNames.DeliveryApi, options =>
{
    options.JsonSerializerOptions.MaxDepth = 128;
});

var app = builder.Build();

await app.BootUmbracoAsync();

app.UseCorrelationSharedModules();

app.MapDefaultEndpoints();

if (!app.Environment.IsProduction())
{
    app.MapScalarApiReference(options =>
    {
        options.OpenApiRoutePattern = "/umbraco/swagger/{documentName}/swagger.json";
    });
}

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseInstallerEndpoints();
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
