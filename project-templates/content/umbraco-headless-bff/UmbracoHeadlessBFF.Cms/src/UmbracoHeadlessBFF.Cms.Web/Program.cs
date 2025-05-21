using Azure.Storage.Blobs;
using Scalar.AspNetCore;
using Umbraco.Cms.Api.Common.DependencyInjection;
using Umbraco.Cms.Core;
using UmbracoHeadlessBFF.Cms.Modules.Common.Links;
using UmbracoHeadlessBFF.Cms.Modules.Common.Preview;
using UmbracoHeadlessBFF.Cms.Modules.Common.Urls;
using UmbracoHeadlessBFF.SharedModules.Common.Correlation;
using UmbracoHeadlessBFF.SharedModules.Common.Environment;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .AddAzureBlobMediaFileSystem()
    .AddAzureBlobImageSharpCache()
    .Build();

builder.AddServiceDefaults();

builder.AddCorrelationSharedModules();
builder.AddPreview();
builder.AddLinks();
builder.AddUrls();

if (environment.IsLocal())
{
    configuration.AddUserSecrets<Program>();
    builder.AddAzureBlobClient("blobs");
}

builder.Services.AddControllers().AddJsonOptions(Constants.JsonOptionsNames.DeliveryApi, options =>
{
    options.JsonSerializerOptions.MaxDepth = 128;
});

var app = builder.Build();

if (environment.IsLocal())
{
    var blobService = app.Services.GetRequiredService<BlobServiceClient>();
    var umbracoMediaContainer = blobService.GetBlobContainerClient(app.Configuration["Umbraco:Storage:AzureBlob:Media:ContainerName"]);

    await umbracoMediaContainer.CreateIfNotExistsAsync();
}

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
