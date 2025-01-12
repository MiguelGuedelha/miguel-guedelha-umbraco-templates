using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

var smtpUser = builder.AddParameter("SmtpUser");
var smtpPassword = builder.AddParameter("SmtpPassword", true);
var smtpPort = builder.AddParameter("SmtpPort");

var mailServer = builder.AddContainer("MailServer", "rnwood/smtp4dev")
    .WithHttpEndpoint(34523, 80, "ui")
    .WithHttpEndpoint(int.Parse(smtpPort.Resource.Value), 25, "smtp")
    .WithVolume("UmbracoBFFAstro-mail-server-data", "/stmp4dev")
    .WithEnvironment("ServerOptions__AuthenticationRequired", "true")
    .WithEnvironment("ServerOptions__Users__0__Username", smtpUser)
    .WithEnvironment("ServerOptions__Users__0__Password", smtpPassword);

var database = builder
    .AddSqlServer("Db")
    .WithDataVolume("UmbracoBFFAstro-db-data")
    .WithVolume("UmbracoBFFAstro-db-log", "/var/opt/mssql/log")
    .WithVolume("UmbracoBFFAstro-db-secrets", "/var/opt/mssql/secrets")
    .WithContainerRuntimeArgs("--user", "root");

var umbracoDb = database.AddDatabase("umbracoDbDSN", "umbraco-cms");

var cache = builder
    .AddRedis("Cache")
    .WithDataVolume("UmbracoBFFAstro-cache-data")
    .WithPersistence(TimeSpan.FromMinutes(5), 100)
    .WithRedisInsight();

var cms = builder.AddProject<Projects.UmbracoBFFAstro_Cms_Web>(
        "Cms",
        launchProfileName: "Cms")
    .WithExternalHttpEndpoints()
    .WithReference(umbracoDb)
    .WithReference(cache)
    .WithEnvironment("Umbraco:CMS:Global:Smtp:Port", smtpPort)
    .WithEnvironment("Umbraco:CMS:Global:Smtp:Username", smtpUser)
    .WithEnvironment("Umbraco:CMS:Global:Smtp:Password", smtpPassword)
    .WaitFor(mailServer)
    .WaitFor(database)
    .WaitFor(umbracoDb)
    .WaitFor(cache);

var cmsDeliveryApi = builder.AddProject<Projects.UmbracoBFFAstro_Cms_Web>(
        "DeliveryApi",
        launchProfileName: "DeliveryApi")
    .WithExternalHttpEndpoints()
    .WithReference(umbracoDb)
    .WithReference(cache)
    .WithEnvironment("Umbraco:CMS:Global:Smtp:Port", smtpPort)
    .WithEnvironment("Umbraco:CMS:Global:Smtp:Username", smtpUser)
    .WithEnvironment("Umbraco:CMS:Global:Smtp:Password", smtpPassword)
    .WaitFor(mailServer)
    .WaitFor(database)
    .WaitFor(umbracoDb)
    .WaitFor(cache)
    .WithReplicas(2);

var siteApi = builder.AddProject<Projects.UmbracoBFFAstro_SiteApi_Web>("SiteApi")
    .WithExternalHttpEndpoints()
    .WithReference(cmsDeliveryApi)
    .WithReference(cache)
    .WaitFor(cmsDeliveryApi)
    .WaitFor(cache);

var frontend = builder.AddPnpmApp("Astro", "../../../UmbracoBFFAstro.Frontend", "dev")
    .WithReference(siteApi)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .WaitFor(siteApi);

builder.Build().Run();
