using FluentValidation;
using Project_APBD.RequestModels;

namespace Project_APBD.Validators;

public class UpdateCompanyCustomerValidator : AbstractValidator<UpdateCompanyCustomerRequest>
{
    public UpdateCompanyCustomerValidator()
    {
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(30);
        RuleFor(x => x.PhoneNumber).NotEmpty().Matches(@"^\d{9}$").WithMessage("Phone number must be exactly 9 digits.");
    }
}