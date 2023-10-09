using BFF.Api.Context;
using BFF.Api.Extensions;
using Microsoft.OpenApi.Models;
using Rubic.AspNetCore;

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
        Title = $"BFF.Api",
        Version = "v1",
    });

    x.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Work Context
builder.Services.AddTransient<IWorkContext, WorkContext>();

// Rubic
builder.Services.AddRubicAspNetCore();
builder.Services.AddRubicSecurityConfiguration(builder.Configuration);
builder.Services.AddRubicJwtAuthentication(builder.Configuration.GetValue<string>("Rubic:Security:SecretKey"));
builder.Services.AddRubicSHA1CryptographyProvider(builder.Configuration.GetValue<string>("Rubic:Security:SecretKey"));
builder.Services.AddBffServices(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.DefaultModelsExpandDepth(-1); // Disable swagger schemas at bottom
});

app.UseRubicAspNetCore();
app.UseRubicAuthentication();
app.UseStaticFiles();
app.MapControllers();
app.Run();