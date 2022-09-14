using FluentValidation;

namespace CRM_System.BusinessLayer;

public class TransferTransactionRequestValidator : AbstractValidator<TransferTransactionRequest>
{
    public TransferTransactionRequestValidator()
    {
        RuleFor(v => v.SenderAccountId)
            .NotEmpty()
            .WithMessage("Fill in the field")
            .GreaterThan(0);

        RuleFor(v => v.RecipientAccountId)
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

        RuleFor(v => v.TradingCurrency)
            .NotEmpty()
            .WithMessage("Fill in the field")
            .IsInEnum()
            .WithMessage("Invalid TradingCurrency");
    }
}

