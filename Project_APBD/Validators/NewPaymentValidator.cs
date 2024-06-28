using FluentValidation;
using Project_APBD.RequestModels;

namespace Project_APBD.Validators;

public class NewPaymentValidator : AbstractValidator<CreatePaymentRequest>
{
    public NewPaymentValidator()
    {
        RuleFor(x => x.ContractId).GreaterThan(0);
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.PaymentType).IsInEnum();
    }
}