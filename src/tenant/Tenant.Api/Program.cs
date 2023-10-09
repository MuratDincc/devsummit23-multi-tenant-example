using Microsoft.OpenApi.Models;
using Rubic.AspNetCore;
using Rubic.Caching;
using Tenant.Api.Middleware;
using Tenant.Application;
using Tenant.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = $"Tenant.Api",
        Version = "v1",
    });
});

builder.Services.AddRubicAspNetCore();
builder.Services.AddRubicCaching(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseMiddleware<DatabaseInstallerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.DefaultModelsExpandDepth(-1); // Disable swagger schemas at bottom
});

app.UseDeveloperExceptionPage();
app.UseRubicAspNetCore();
app.MapControllers();
app.Run();
