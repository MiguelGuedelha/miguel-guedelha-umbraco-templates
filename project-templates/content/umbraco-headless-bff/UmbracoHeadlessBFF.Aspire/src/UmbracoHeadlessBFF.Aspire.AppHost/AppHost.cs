using Microsoft.Extensions.Configuration;
using UmbracoHeadlessBFF.SharedModules.Common.Caching;
using UmbracoHeadlessBFF.SharedModules.Common.Environment;
using UmbracoHeadlessBFF.SharedModules.Common.ServiceDiscovery;

var builder = DistributedApplication.CreateBuilder(args);

if (builder.Environment.IsLocal())
{
    builder.Configuration.AddUserSecrets<Program>();
}

var smtpUser = builder.AddParameter("SmtpUser");
var smtpPassword = builder.AddParameter("SmtpPassword", true);
var smtpPort = builder.AddParameter("SmtpPort");

var smtpPortString = await smtpPort.Resource.GetValueAsync(CancellationToken.None);

const string baseBindPath = "../../../local-data/v17/";

var mailServer = builder.AddContainer(Services.SmtpServer, "rnwood/smtp4dev")
    .WithHttpEndpoint(34523, 80, "ui")
    .WithHttpEndpoint(int.Parse(smtpPortString!), 25, "smtp")
    .WithBindMount(Path.Join(baseBindPath, "mail-server/data"), "/stmp4dev")
    .WithEnvironment("ServerOptions__AuthenticationRequired", "true")
    .WithEnvironment("ServerOptions__Users__0__Username", smtpUser)
    .WithEnvironment("ServerOptions__Users__0__Password", smtpPassword);

smtpUser.WithParentRelationship(mailServer);
smtpPassword.WithParentRelationship(mailServer);
smtpPort.WithParentRelationship(mailServer);

var database = builder
    .AddSqlServer(Services.DatabaseServer)
    .WithDataBindMount(Path.Join(baseBindPath, "database/data"))
    .WithContainerRuntimeArgs("--user", "root")
    .WithLifetime(ContainerLifetime.Persistent);

var umbracoDb = database.AddDatabase(Services.Database, "umbraco-cms");

var cache = builder
    .AddRedis(CachingConstants.ConnectionStringName)
    .WithRedisInsight();

var azureStorage = builder
    .AddAzureStorage(Services.AzureStorage)
    .RunAsEmulator(o =>
    {
        o.WithLifetime(ContainerLifetime.Persistent);
        o.WithDataBindMount(Path.Join(baseBindPath, "azure-storage/data"));
    });


var cmsUmbracoBlobContainerParameter = builder.AddParameter("CmsUmbracoBlobContainer");
var blobContainerValue = await cmsUmbracoBlobContainerParameter.Resource.GetValueAsync(CancellationToken.None);

var umbracoMediaBlob = azureStorage.AddBlobContainer(blobContainerValue!);

cmsUmbracoBlobContainerParameter.WithParentRelationship(umbracoMediaBlob);

var serviceBus = builder
    .AddAzureServiceBus(Services.ServiceBus.Name)
    .RunAsEmulator(o =>
    {
        o.WithLifetime(ContainerLifetime.Persistent);
    });

var cmsCacheTopic = serviceBus.AddServiceBusTopic(Services.ServiceBus.Topics.CmsCache);

cmsCacheTopic.AddServiceBusSubscription(Services.ServiceBus.Subscriptions.SiteApiCmsCache)
    .WithProperties(sub =>
    {
        sub.MaxDeliveryCount = 5;
    });

var cms = builder.AddProject<Projects.Cms>(Services.Cms, launchProfileName: "single")
    .WithExternalHttpEndpoints();

var cmsDeliveryApiKey = builder.AddParameter("CmsDeliveryApiKey");

cms.WithReference(umbracoDb, connectionName: "umbracoDbDSN")
    .WithReference(cache)
    .WithReference(serviceBus)
    .WithEnvironment("Umbraco__CMS__Global__Smtp__Port", smtpPort)
    .WithEnvironment("Umbraco__CMS__Global__Smtp__Username", smtpUser)
    .WithEnvironment("Umbraco__CMS__Global__Smtp__Password", smtpPassword)
    .WithEnvironment("Umbraco__Storage__AzureBlob__Media__ConnectionString", umbracoMediaBlob.Resource.Parent.ConnectionStringExpression)
    .WithEnvironment("Umbraco__Storage__AzureBlob__Media__ContainerName", cmsUmbracoBlobContainerParameter)
    .WithEnvironment("Umbraco__CMS__DeliveryApi__ApiKey", cmsDeliveryApiKey)
    .WithEnvironment("ApplicationUrls__Media", () => cms.Resource.GetEndpoint("https").Url)
    .WaitFor(mailServer)
    .WaitFor(umbracoDb)
    .WaitFor(cache)
    .WaitFor(umbracoMediaBlob)
    .WaitFor(serviceBus);

cmsDeliveryApiKey.WithParentRelationship(cms);

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

var siteApi = builder.AddProject<Projects.SiteApi>(Services.SiteApi)
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

await builder.Build().RunAsync();
