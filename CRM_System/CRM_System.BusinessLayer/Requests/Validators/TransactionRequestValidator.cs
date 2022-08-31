using FluentValidation;

namespace CRM_System.BusinessLayer;

public class TransactionRequestValidator : AbstractValidator<TransactionRequest>
{
    public TransactionRequestValidator()
    {
        RuleFor(v => v.AccountId)
            .NotEmpty()
            .WithMessage("Fill in the field")
            .GreaterThan(0);

        RuleFor(v => v.TransactionType)
            .NotEmpty()
            .WithMessage("Fill in the field")
            .IsInEnum()
            .WithMessage("Invalid transaction's type");

        RuleFor(v => v.Amount)
            .NotEmpty()
            .WithMessage("Fill in the field")
            .GreaterThan(0)
            .WithMessage("Amount must be greater than zero");

        RuleFor(v => v.Currency)
            .NotEmpty()
            .WithMessage("Fill in the field")
            .IsInEnum()
            .WithMessage("Invalid currency");
    }
}

