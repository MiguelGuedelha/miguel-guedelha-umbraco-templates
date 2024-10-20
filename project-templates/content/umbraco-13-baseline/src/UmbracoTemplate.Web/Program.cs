using CorrelationId;
using CorrelationId.DependencyInjection;
using Serilog;

#if UseNodeReactFrontend
using NodeReact;
#endif

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
#if UseContentDeliveryApi
    .AddDeliveryApi()
#endif
    .AddComposers()
    .Build();

if (bool.TryParse(Environment.GetEnvironmentVariable("USE_USER_SECRETS"), out var useUserSecrets)
    && useUserSecrets)
{
    configuration.AddUserSecrets<Program>();
}

builder.AddServiceDefaults();

services.AddCorrelationId(o =>
{
    o.AddToLoggingScope = true;
})
.WithGuidProvider();

#if(UseLoadBalancing || UseCaching)
builder.AddRedisDistributedCache("cache");
#endif

#if UseNodeReactFrontend
builder.Services.AddMvc().AddRazorRuntimeCompilation();

// Needs update of dependencies
// Follow here: https://github.com/DaniilSokolyuk/NodeReact.NET/pull/15
builder.Services.AddNodeReact(
    config =>
    {
        config.EnginesCount = 2;
        config.ConfigureOutOfProcessNodeJSService(o =>
        {
            o.NumRetries = 0;
            o.InvocationTimeoutMS = 10000;
        });
        config.AddScriptWithoutTransform("~/server.bundle.js");
        config.UseDebugReact = true;

        config.ConfigureSystemTextJsonPropsSerializer(_ => { });
        //config.ConfigureNewtonsoftJsonPropsSerializer(_ => { });
    }
);
#endif

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

app.UseCorrelationId();

// Run app
try
{
    await app.RunAsync();
}
catch (Exception e)
{
    Log.Fatal(e, "Host shutdown unexpectedly");
}
finally
{
    Log.Information("Flushing Log");

    Log.CloseAndFlush();
}
