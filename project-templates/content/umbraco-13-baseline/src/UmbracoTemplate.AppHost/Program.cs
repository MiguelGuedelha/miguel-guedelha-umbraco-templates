using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

var smtpUser = builder.AddParameter("SmtpUser");
var smtpPassword = builder.AddParameter("SmtpPassword", true);
var smtpPort = builder.AddParameter("SmtpPort");

var testMailServer = builder.AddContainer("mail-server", "rnwood/smtp4dev")
    .WithHttpEndpoint(34523, 80, "ui")
    .WithHttpEndpoint(int.Parse(smtpPort.Resource.Value), 25, "smtp")
    .WithVolume("UmbracoTemplate-smtp4dev-data", "/stmp4dev")
    .WithEnvironment("ServerOptions__AuthenticationRequired", "true")
    .WithEnvironment("ServerOptions__Users__0__Username", smtpUser)
    .WithEnvironment("ServerOptions__Users__0__Password", smtpPassword);

var sqlServer = builder
    .AddSqlServer("sqlserver")
    .WithDataVolume("UmbracoTemplate-mssql-data")
    .WithVolume("UmbracoTemplate-mssql-log", "/var/opt/mssql/log")
    .WithVolume("UmbracoTemplate-mssql-secrets", "/var/opt/mssql/secrets")
    .WithContainerRuntimeArgs("--user", "root");

var umbracoDb = sqlServer.AddDatabase("umbracoDbDSN", "umbraco-cms");

var cms = builder.AddProject<Projects.UmbracoTemplate_Cms>("cms")
    .WithExternalHttpEndpoints()
    .WithReference(umbracoDb)
    .WithEnvironment("Umbraco:CMS:Global:Smtp:Port", smtpPort)
    .WithEnvironment("Umbraco:CMS:Global:Smtp:Username", smtpUser)
    .WithEnvironment("Umbraco:CMS:Global:Smtp:Password", smtpPassword);

#if UseAstroFrontend
var astroApp = builder.AddPnpmApp("astro", "../frontend", "dev")
    .WithReference(cms)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints();
#endif

builder.Build().Run();
