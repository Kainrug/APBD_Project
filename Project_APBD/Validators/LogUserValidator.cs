using FluentValidation;
using Project_APBD.RequestModels;

namespace Project_APBD.Validators;

public class LogUserValidator : AbstractValidator<LoginRequest>
{
    public LogUserValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(50);
    }
}