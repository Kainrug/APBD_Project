using FluentValidation;
using Project_APBD.Models;
using Project_APBD.RequestModels;

namespace Project_APBD.Validators;

public class NewUserValidator : AbstractValidator<RegisterRequest>
{
    public NewUserValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(e => e.Email).NotEmpty().EmailAddress();
    }
}