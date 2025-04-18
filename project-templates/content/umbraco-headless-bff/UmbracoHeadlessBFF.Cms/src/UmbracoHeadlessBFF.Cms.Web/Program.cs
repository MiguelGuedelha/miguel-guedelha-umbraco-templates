using Azure.Storage.Blobs;
using Umbraco.Cms.Api.Common.DependencyInjection;
using Umbraco.Cms.Core;
using UmbracoHeadlessBFF.SharedModules.Common.Caching;
using UmbracoHeadlessBFF.SharedModules.Common.Correlation;

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

builder.AddCaching();
builder.AddCorrelation();

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

app.UseCorrelation();

app.MapDefaultEndpoints();

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
