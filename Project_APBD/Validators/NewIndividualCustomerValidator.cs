using FluentValidation;
using Project_APBD.RequestModels;

namespace Project_APBD.Validators;

public class NewIndividualCustomerValidator : AbstractValidator<CreateIndividualCustomerRequest>
{
    public NewIndividualCustomerValidator()
    {
        RuleFor(e => e.FirstName).NotEmpty().MaximumLength(30);
        RuleFor(e => e.LastName).NotEmpty().MaximumLength(30);
        RuleFor(e => e.Address).NotEmpty().MaximumLength(40);
        RuleFor(e => e.Email).NotEmpty().MaximumLength(50).EmailAddress();
        RuleFor(e => e.Pesel).NotEmpty().MaximumLength(11);
        RuleFor(e => e.PhoneNumber).NotEmpty().MaximumLength(9).Matches(@"^\d{9}$");
    }
}