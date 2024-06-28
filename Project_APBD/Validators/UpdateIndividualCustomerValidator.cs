using FluentValidation;
using Project_APBD.RequestModels;

namespace Project_APBD.Validators;

public class UpdateIndividualCustomerValidator : AbstractValidator<UpdateIndividualCustomerRequest>
{
    public UpdateIndividualCustomerValidator()
    {
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.PhoneNumber).NotEmpty().Matches(@"^\d{9}$").WithMessage("Phone number must be exactly 9 digits.");
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(30);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(30);
        
    }
}