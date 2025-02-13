using UmbracoBFFAstro.SharedModules.Features.Caching;
using UmbracoBFFAstro.SharedModules.Features.Correlation;
using UmbracoBFFAstro.SharedModules.Features.Environment;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddServiceDefaults();

builder.AddCaching(configuration);
builder.AddCorrelation();

if (environment.IsLocal())
{
    configuration.AddUserSecrets<Program>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCorrelation();

app.Run();
