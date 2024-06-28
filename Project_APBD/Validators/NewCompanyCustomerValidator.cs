using FluentValidation;
using Project_APBD.RequestModels;

namespace Project_APBD.Validators;

public class NewCompanyCustomerValidator : AbstractValidator<CreateCompanyCustomerRequest>
{
    public NewCompanyCustomerValidator()
    {
        RuleFor(e => e.Email).NotEmpty().EmailAddress();
        RuleFor(e => e.Address).NotEmpty().MaximumLength(30);
        RuleFor(e => e.CompanyName).NotEmpty().MaximumLength(30);
        RuleFor(e => e.KrsNumber).NotEmpty().MaximumLength(10);
        RuleFor(e => e.PhoneNumber).NotEmpty().MaximumLength(9).Matches(@"^\d{9}$");
    }
}