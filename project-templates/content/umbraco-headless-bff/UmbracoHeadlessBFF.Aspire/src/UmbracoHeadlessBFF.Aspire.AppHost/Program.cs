using Microsoft.Extensions.Configuration;
using UmbracoHeadlessBFF.SharedModules.Common.Caching;
using UmbracoHeadlessBFF.SharedModules.Common.Environment;

var builder = DistributedApplication.CreateBuilder(args);

if (builder.Environment.IsLocal())
{
    builder.Configuration.AddUserSecrets<Program>();
}

var smtpUser = builder.AddParameter("SmtpUser");
var smtpPassword = builder.AddParameter("SmtpPassword", true);
var smtpPort = builder.AddParameter("SmtpPort");

var mailServer = builder.AddContainer("MailServer", "rnwood/smtp4dev")
    .WithHttpEndpoint(34523, 80, "ui")
    .WithHttpEndpoint(int.Parse(smtpPort.Resource.Value), 25, "smtp")
    .WithVolume("UmbracoHeadlessBFF-mail-server-data", "/stmp4dev")
    .WithEnvironment("ServerOptions__AuthenticationRequired", "true")
    .WithEnvironment("ServerOptions__Users__0__Username", smtpUser)
    .WithEnvironment("ServerOptions__Users__0__Password", smtpPassword);

var database = builder
    .AddSqlServer("SqlServer")
    .WithDataVolume("UmbracoHeadlessBFF-db-data")
    .WithVolume("UmbracoHeadlessBFF-db-log", "/var/opt/mssql/log")
    .WithVolume("UmbracoHeadlessBFF-db-secrets", "/var/opt/mssql/secrets")
    .WithContainerRuntimeArgs("--user", "root");

var umbracoDb = database.AddDatabase("Database", "umbraco-cms");

var cache = builder
    .AddRedis(CachingConstants.ConnectionStringName)
    .WithRedisInsight();

var blobStorage = builder.AddAzureStorage("BlobStorage");

if (builder.Environment.IsLocal())
{
    blobStorage.RunAsEmulator(c =>
    {
        c.WithDataVolume("UmbracoHeadlessBFF-blob-storage");
    });
}

var umbracoBlob = blobStorage.AddBlobs("blobs");

var cmsDeliveryApiKey = builder.AddParameter("CmsDeliveryApiKey");

#if (false)
// Don't commit actual name as below, it should not be compilable inside this template
// Only compilable when testing/running during template development
// It should always be GeneratedClassNamePrefix_Cms_Web when committed to remote
#endif
var cms = builder.AddProject<Projects.GeneratedClassNamePrefix_Cms_Web>("Cms", launchProfileName: "single");

cms.WithExternalHttpEndpoints()
    .WithReference(umbracoDb, connectionName: "umbracoDbDSN")
    .WithReference(cache)
    //Only needed to add this reference on local so we can connect to the client in a standard way and ensure the blob container exists before booting up umbraco
    .WithReference(umbracoBlob)
    .WithEnvironment("Umbraco__CMS__Global__Smtp__Port", smtpPort)
    .WithEnvironment("Umbraco__CMS__Global__Smtp__Username", smtpUser)
    .WithEnvironment("Umbraco__CMS__Global__Smtp__Password", smtpPassword)
    .WithEnvironment("Umbraco__Storage__AzureBlob__Media__ConnectionString", umbracoBlob.Resource.ConnectionStringExpression)
    .WithEnvironment("Umbraco__CMS__DeliveryApi__ApiKey", cmsDeliveryApiKey)
    .WithEnvironment("ApplicationUrls__Media", () => cms.Resource.GetEndpoint("https").Url)
    .WaitFor(mailServer)
    .WaitFor(umbracoDb)
    .WaitFor(cache)
    .WaitFor(umbracoBlob);

cms.WithUrls(context =>
{
    var httpsEndpoint = cms.GetEndpoint("https");
    var httpsEndpointUrl = httpsEndpoint.Url;

    context.Urls.Clear();
    context.Urls.Add(new() { Url = $"{httpsEndpointUrl}/umbraco", DisplayText = "Umbraco Dashboard", Endpoint = httpsEndpoint });
    context.Urls.Add(new() { Url = $"{httpsEndpointUrl}/scalar/delivery", DisplayText = "Scalar - Delivery API", Endpoint = httpsEndpoint });
    context.Urls.Add(new() { Url = $"{httpsEndpointUrl}/scalar/default", DisplayText = "Scalar - Default API", Endpoint = httpsEndpoint });
    context.Urls.Add(new() { Url = $"{httpsEndpointUrl}/umbraco/swagger/index.html?urls.primaryName=Umbraco+Delivery+API", DisplayText = "Swagger - Delivery API", Endpoint = httpsEndpoint });
    context.Urls.Add(new() { Url = $"{httpsEndpointUrl}/umbraco/swagger/index.html", DisplayText = "Swagger - Default API", Endpoint = httpsEndpoint });
});

#if (false)
// Don't commit actual name as below, it should not be compilable inside this template
// Only compilable when testing/running during template development
// It should always be GeneratedClassNamePrefix_SiteApi_Web when committed to remote
#endif
var siteApi = builder.AddProject<Projects.GeneratedClassNamePrefix_SiteApi_Web>("SiteApi")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(cms)
    .WithEnvironment("services__Cms__Parameters__DeliveryApiKey", cmsDeliveryApiKey)
    .WithEnvironment("ApplicationUrls__Media", () => cms.Resource.GetEndpoint("https").Url)
    .WaitFor(cache)
    .WaitFor(cms);

siteApi.WithUrls(context =>
{
    var httpsEndpoint = siteApi.GetEndpoint("https");
    var httpsEndpointUrl = httpsEndpoint.Url;

    context.Urls.Clear();
    context.Urls.Add(new() { Url = $"{httpsEndpointUrl}/scalar", DisplayText = "Scalar - Site Api v1", Endpoint = httpsEndpoint });
});

// Example frontend service that could be added to aspire orchestration
// Delete if not desired
// var frontend = builder.AddPnpmApp("frontend-astro", "../../../UmbracoHeadlessBFF.Frontend", "dev")
//     .WithPnpmPackageInstallation()
//     .WithReference(siteApi)
//     .WithHttpEndpoint(targetPort: 4321)
//     .WithExternalHttpEndpoints()
//     .WaitFor(siteApi);

await builder.Build().RunAsync();
