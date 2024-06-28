using FluentValidation;
using Project_APBD.RequestModels;

namespace Project_APBD.Validators;

public class NewContractValidator : AbstractValidator<CreateContractRequest>
{
    public NewContractValidator()
    {
        RuleFor(x => x.SoftwareId)
            .GreaterThan(0).WithMessage("Software ID must be a positive number.");

        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("Customer ID must be a positive number.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required.")
            .GreaterThan(x => x.StartDate).WithMessage("End date must be after start date.")
            .Must((request, endDate) => (endDate - request.StartDate).Days >= 3 && (endDate - request.StartDate).Days <= 30)
            .WithMessage("Contract duration must be between 3 and 30 days.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.SupportYears)
            .InclusiveBetween(1, 4).WithMessage("Support years must be between 1 and 4.");

        RuleFor(x => x.IsSigned)
            .NotNull().WithMessage("IsSigned status is required.");
    }
}