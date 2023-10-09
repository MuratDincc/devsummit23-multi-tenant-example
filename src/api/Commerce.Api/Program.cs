using Commerce.Api.Context;
using Commerce.Api.Middleware;
using Commerce.Api.Swagger;
using Commerce.Application;
using Commerce.Infrastructure;
using Microsoft.OpenApi.Models;
using Rubic.AspNetCore;
using Rubic.Caching;

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
        Title = $"Commerce.Api",
        Version = "v1",
    });
    x.OperationFilter<AddRequiredHeaderParameterAttribute>();
});

builder.Services.AddScoped<Commerce.Infrastructure.Abstracts.IWorkContext, WorkContext>();

builder.Services.AddRubicAspNetCore();
builder.Services.AddRubicCaching(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.DefaultModelsExpandDepth(-1); // Disable swagger schemas at bottom
});

app.UseRubicAspNetCore();
app.MapControllers();
app.UseMiddleware<DatabaseInstallerMiddleware>();
app.Run();
