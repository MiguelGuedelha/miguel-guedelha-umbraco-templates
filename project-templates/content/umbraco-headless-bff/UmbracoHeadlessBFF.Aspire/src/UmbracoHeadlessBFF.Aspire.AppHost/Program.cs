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

const string baseBindPath = "../../../local-data/v16";

var mailServer = builder.AddContainer("MailServer", "rnwood/smtp4dev")
    .WithHttpEndpoint(34523, 80, "ui")
    .WithHttpEndpoint(int.Parse(smtpPort.Resource.Value), 25, "smtp")
    .WithBindMount(Path.Join(baseBindPath, "mail-server/data"), "/stmp4dev")
    .WithEnvironment("ServerOptions__AuthenticationRequired", "true")
    .WithEnvironment("ServerOptions__Users__0__Username", smtpUser)
    .WithEnvironment("ServerOptions__Users__0__Password", smtpPassword);

var database = builder
    .AddSqlServer("SqlServer")
    .WithDataBindMount(Path.Join(baseBindPath, "database/data"))
    .WithBindMount(Path.Join(baseBindPath, "database/logs"), "/var/opt/mssql/log")
    .WithBindMount(Path.Join(baseBindPath, "database/secrets"), "/var/opt/mssql/secrets")
    .WithContainerRuntimeArgs("--user", "root");

var umbracoDb = database.AddDatabase("Database", "umbraco-cms");

var cache = builder
    .AddRedis(CachingConstants.ConnectionStringName)
    .WithRedisInsight();

var azureStorage = builder
    .AddAzureStorage("Storage")
    .RunAsEmulator(c =>
    {
        c.WithDataBindMount(Path.Join(baseBindPath, "azure-storage/data"));
    });

var blobs = azureStorage.AddBlobs("blobs");

var cmsUmbracoBlobContainer = builder.AddParameter("CmsUmbracoBlobContainer");

var umbracoMediaBlob = blobs.AddBlobContainer(cmsUmbracoBlobContainer.Resource.Value);

var cmsDeliveryApiKey = builder.AddParameter("CmsDeliveryApiKey");

var serviceBus = builder
    .AddAzureServiceBus("ServiceBus")
    .RunAsEmulator(sb =>
    {
        // See https://github.com/dotnet/aspire/issues/8818 for details of what is happening here
        // Will remove if Aspire team provides option to customise DB built-in eventually: https://github.com/dotnet/aspire/issues/9279
        sb.WithHttpEndpoint(targetPort: 5300, name: "sbhealthendpoint")
            .WithImageTag("1.1.2")
            .WithContainerName("servicebus")
            .WithEnvironment("SQL_WAIT_INTERVAL", "1");

        var edge = sb.ApplicationBuilder.Resources.OfType<ContainerResource>()
            .First(resource => resource.Name.EndsWith("-sqledge"));

        var annotation = edge.Annotations.OfType<ContainerImageAnnotation>().First();

        annotation.Image = "mssql/server";
        annotation.Tag = "2022-latest";
    });

// See https://github.com/dotnet/aspire/issues/8818 for details of what is happening here
// Will remove if Aspire team provides option to customise DB built-in eventually: https://github.com/dotnet/aspire/issues/9279
var sbHc = serviceBus.Resource.Annotations.OfType<HealthCheckAnnotation>().First();
serviceBus.Resource.Annotations.Remove(sbHc);
serviceBus.WithHttpHealthCheck("/health", 200, "sbhealthendpoint");

var cmsCacheTopic = serviceBus.AddServiceBusTopic("CmsCacheTopic");

cmsCacheTopic.AddServiceBusSubscription("CmsCacheTopicSiteApiSub")
    .WithProperties(sub =>
    {
        sub.MaxDeliveryCount = 5;
    });

#if (false)
// Don't commit actual name as below, it should not be compilable inside this template
// Only compilable when testing/running during template development
// It should always be GeneratedClassNamePrefix_Cms_Web when committed to remote
#endif
var cms = builder.AddProject<Projects.GeneratedClassNamePrefix_Cms_Web>("Cms", launchProfileName: "single")
    .WithExternalHttpEndpoints();

cms.WithReference(umbracoDb, connectionName: "umbracoDbDSN")
    .WithReference(cache)
    .WithReference(serviceBus)
    .WithEnvironment("Umbraco__CMS__Global__Smtp__Port", smtpPort)
    .WithEnvironment("Umbraco__CMS__Global__Smtp__Username", smtpUser)
    .WithEnvironment("Umbraco__CMS__Global__Smtp__Password", smtpPassword)
    .WithEnvironment("Umbraco__Storage__AzureBlob__Media__ConnectionString", blobs)
    .WithEnvironment("Umbraco__Storage__AzureBlob__Media__ContainerName", cmsUmbracoBlobContainer)
    .WithEnvironment("Umbraco__CMS__DeliveryApi__ApiKey", cmsDeliveryApiKey)
    .WithEnvironment("ApplicationUrls__Media", () => cms.Resource.GetEndpoint("https").Url)
    .WaitFor(mailServer)
    .WaitFor(umbracoDb)
    .WaitFor(cache)
    .WaitFor(umbracoMediaBlob)
    .WaitFor(serviceBus);

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
    .WithReference(serviceBus)
    .WithEnvironment("services__Cms__Parameters__DeliveryApiKey", cmsDeliveryApiKey)
    .WithEnvironment("ApplicationUrls__Media", () => cms.Resource.GetEndpoint("https").Url)
    .WaitFor(cache)
    .WaitFor(cms)
    .WaitFor(serviceBus);

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
