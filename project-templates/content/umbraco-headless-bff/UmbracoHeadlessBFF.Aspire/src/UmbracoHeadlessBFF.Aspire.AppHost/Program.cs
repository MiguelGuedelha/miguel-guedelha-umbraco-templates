using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

if (builder.Environment.IsLocal())
{
    builder.Configuration.AddUserSecrets<Program>();
}

var smtpUser = builder.AddParameter("SmtpUser");
var smtpPassword = builder.AddParameter("SmtpPassword", true);
var smtpPort = builder.AddParameter("SmtpPort");

var mailServer = builder.AddContainer("mail-server", "rnwood/smtp4dev")
    .WithHttpEndpoint(34523, 80, "ui")
    .WithHttpEndpoint(int.Parse(smtpPort.Resource.Value), 25, "smtp")
    .WithVolume("UmbracoHeadlessBFF-mail-server-data", "/stmp4dev")
    .WithEnvironment("ServerOptions__AuthenticationRequired", "true")
    .WithEnvironment("ServerOptions__Users__0__Username", smtpUser)
    .WithEnvironment("ServerOptions__Users__0__Password", smtpPassword);

var database = builder
    .AddSqlServer("db")
    .WithDataVolume("UmbracoHeadlessBFF-db-data")
    .WithVolume("UmbracoHeadlessBFF-db-log", "/var/opt/mssql/log")
    .WithVolume("UmbracoHeadlessBFF-db-secrets", "/var/opt/mssql/secrets")
    .WithContainerRuntimeArgs("--user", "root");

var umbracoDb = database.AddDatabase("umbracoDbDSN", "umbraco-cms");

var cache = builder
    .AddRedis("cache")
    .WithRedisInsight();

var blobStorage = builder.AddAzureStorage("blob-storage");

if (builder.Environment.IsLocal())
{
    blobStorage.RunAsEmulator(c =>
    {
        c.WithDataVolume("UmbracoHeadlessBFF-blob-storage");
    });
}

var umbracoBlob = blobStorage.AddBlobs("blobs");

#if (false)
// Don't commit actual name as below, it should not be compilable inside this template
// Only compilable when testing/running during template development
// It should always be GeneratedClassNamePrefix_Cms_Web
#endif
var cms = builder.AddProject<Projects.GeneratedClassNamePrefix_Cms_Web>("cms", launchProfileName: "single")
    .WithExternalHttpEndpoints()
    .WithReference(umbracoDb)
    .WithReference(cache)
    .WithEnvironment("Umbraco__CMS__Global__Smtp__Port", smtpPort)
    .WithEnvironment("Umbraco__CMS__Global__Smtp__Username", smtpUser)
    .WithEnvironment("Umbraco__CMS__Global__Smtp__Password", smtpPassword)
    .WithEnvironment("Umbraco__Storage__AzureBlob__Media__ConnectionString", umbracoBlob.Resource.ConnectionStringExpression)
    .WaitFor(mailServer)
    .WaitFor(umbracoDb)
    .WaitFor(cache)
    .WaitFor(umbracoBlob);

#if (false)
// Don't commit actual name as below, it should not be compilable inside this template
// Only compilable when testing/running during template development
// It should always be GeneratedClassNamePrefix_SiteApi_Web
#endif
var siteApi = builder.AddProject<Projects.GeneratedClassNamePrefix_SiteApi_Web>("site-api")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(cms)
    .WaitFor(cache)
    .WaitFor(cms);

// Example frontend service that could be added to aspire orchestration
// var frontend = builder.AddPnpmApp("frontend-astro", "../../../UmbracoHeadlessBFF.Frontend", "dev")
//     .WithPnpmPackageInstallation()
//     .WithReference(siteApi)
//     .WithHttpEndpoint(targetPort: 4321)
//     .WithExternalHttpEndpoints()
//     .WaitFor(siteApi);

builder.Build().Run();
