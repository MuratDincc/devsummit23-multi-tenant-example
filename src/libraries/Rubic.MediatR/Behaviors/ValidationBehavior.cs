using FluentValidation;
using MediatR;
using Rubic.AspNetCore.Exceptions;

namespace Rubic.MediatR.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

        if (!failures.Any())
            return await next();

        var groupValidationExceptions = failures.GroupBy(x => x.PropertyName).ToList();

        var validationFailures = groupValidationExceptions.Select(x =>
            new ErrorResultDetail
            {
                Field = x.Key,
                Message = x.Select(y => y.ErrorMessage)
            }).ToList();

        throw new StatusException(StatusCode.BadRequest, validationFailures, "validation.error");
    }
}
