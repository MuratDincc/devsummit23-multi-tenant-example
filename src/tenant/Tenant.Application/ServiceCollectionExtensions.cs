using Microsoft.Extensions.DependencyInjection;
using Rubic.AspNetCore;
using Rubic.MediatR;

namespace Tenant.Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddRubicMediatR(typeof(ServiceCollectionExtensions));
    }
}
