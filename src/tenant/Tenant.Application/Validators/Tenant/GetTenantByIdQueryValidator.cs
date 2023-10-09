using Commerce.Application.Validators;
using FluentValidation;
using Tenant.Application.Queries.Tenant;

namespace Tenant.Application.Validators.Tenant;

public class GetTenantByIdQueryValidator : AbstractValidator<GetTenantByIdQuery>
{
    public GetTenantByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .GreaterThan(-1);
    }
}
