using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Rubic.AspNetCore.Configurations;
using Rubic.AspNetCore.Exceptions;
using Rubic.AspNetCore.Security;
using Rubic.AspNetCore.Security.Cryptography;

namespace Rubic.AspNetCore;

public static class ServiceCollectionExtensions
{
    public static void AddRubicAspNetCore(this IServiceCollection services)
    {
        services.AddControllers(options =>
            {
                options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(typeof(RawErrorResult), StatusCodes.Status404NotFound));
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(typeof(RawErrorResult), StatusCodes.Status400BadRequest));
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(RawErrorResult),
                    StatusCodes.Status500InternalServerError));
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
        
        services.AddHttpContextAccessor();
        
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
    }

    public static void AddRubicSecurityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var rubicSecurityConfiguration = configuration.GetSection("Rubic:Security").Get<RubicSecurityConfiguration>();
        if (rubicSecurityConfiguration == null)
            throw new Exception("Rubic security config error!");

        services.AddSingleton(rubicSecurityConfiguration);
    }
    
    public static void AddRubicJwtAuthentication(this IServiceCollection services, string secretKey = "")
    {
        services.AddAuthorization();
        
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });
        
        services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });
    }
    
    public static void AddRubicSHA1CryptographyProvider(this IServiceCollection services, string encryptionKey = "")
    {
        if (string.IsNullOrWhiteSpace(encryptionKey) || encryptionKey.Length < 16)
            throw new ArgumentException("Encryption Key cannot be null and must be at least 16 characters long.");

        services.AddTransient<ICryptographyProvider>(provider => new SHA1CryptographyProvider(encryptionKey));
    }
}