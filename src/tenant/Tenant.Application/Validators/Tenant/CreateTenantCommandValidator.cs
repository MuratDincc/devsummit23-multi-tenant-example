using FluentValidation;
using Tenant.Application.Commands.Tenant;

namespace Tenant.Application.Validators.Tenant;

public class CreateTenantCommandValidator : AbstractValidator<CreateTenantCommand>
{
    public CreateTenantCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.Slug)
            .NotNull()
            .NotEmpty();
    }
}
