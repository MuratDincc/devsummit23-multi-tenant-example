using Commerce.Application.Validators;
using FluentValidation;
using Tenant.Application.Commands.Tenant;

namespace Tenant.Application.Validators.Tenant;

public class UpdateTenantCommandValidator : AbstractValidator<UpdateTenantCommand>
{
    public UpdateTenantCommandValidator()
    {
        RuleFor(x => x.AliasId)
            .NotNull()
            .SetValidator(new GuidValidator());

        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.Slug)
            .NotNull()
            .NotEmpty();
    }
}
