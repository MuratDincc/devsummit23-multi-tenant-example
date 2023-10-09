using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Rubic.MediatR.Behaviors;

namespace Rubic.MediatR;

public static class ServiceCollectionExtensions
{
    public static void AddRubicMediatR(this IServiceCollection services, params Type[] types)
    {
        services.AddMediatR(x => 
            x.RegisterServicesFromAssembly(types.First().Assembly)
        );

        services.AddValidatorsFromAssembly(types.First().Assembly);
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}
