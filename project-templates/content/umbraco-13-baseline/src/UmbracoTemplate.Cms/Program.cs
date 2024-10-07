var builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

if (bool.TryParse(Environment.GetEnvironmentVariable("USE_USER_SECRETS"), out var useUserSecrets)
    && useUserSecrets)
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.AddServiceDefaults();

var app = builder.Build();

await app.BootUmbracoAsync();

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

app.MapDefaultEndpoints();

await app.RunAsync();
