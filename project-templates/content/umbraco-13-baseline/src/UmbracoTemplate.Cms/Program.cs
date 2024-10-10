using NodeReact;

var builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
#if (UseContentDeliveryApi)
    .AddDeliveryApi()
#endif
    .AddComposers()
    .Build();

if (bool.TryParse(Environment.GetEnvironmentVariable("USE_USER_SECRETS"), out var useUserSecrets)
    && useUserSecrets)
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.AddServiceDefaults();

builder.Services.AddMvc().AddRazorRuntimeCompilation();

#if (UseNodeReactFrontend)
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

await app.RunAsync();
