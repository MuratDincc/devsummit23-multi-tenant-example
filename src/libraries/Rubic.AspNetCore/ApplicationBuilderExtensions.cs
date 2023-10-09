using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Rubic.AspNetCore.Exceptions;

namespace Rubic.AspNetCore;

public static class ApplicationBuilderExtensions
{
    public static void UseRubicAspNetCore(this IApplicationBuilder application)
    {
        application.Use((context, next) =>
        {
            context.Request.EnableBuffering();
            context.Items.Add(CustomExceptionFilterAttribute.LogRequestOnExceptionKey, true);
            return next();
        });
    }
    public static void UseRubicAuthentication(this IApplicationBuilder application)
    {
        application.UseAuthentication();
        application.UseAuthorization();
    }
}