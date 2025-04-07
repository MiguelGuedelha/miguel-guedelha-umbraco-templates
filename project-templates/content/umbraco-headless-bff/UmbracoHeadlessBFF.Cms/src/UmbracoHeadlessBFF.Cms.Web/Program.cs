using Azure.Storage.Blobs;
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

builder.AddCaching(configuration);
builder.AddCorrelation();

if (environment.IsLocal())
{
    configuration.AddUserSecrets<Program>();
    builder.AddAzureBlobClient("blobs");
}

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
