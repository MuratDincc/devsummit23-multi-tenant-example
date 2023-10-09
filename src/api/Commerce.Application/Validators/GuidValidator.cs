using FluentValidation;

namespace Commerce.Application.Validators;

public class GuidValidator : AbstractValidator<Guid>
{
    public GuidValidator()
    {
        RuleFor(x => x).NotEmpty();
    }
}
