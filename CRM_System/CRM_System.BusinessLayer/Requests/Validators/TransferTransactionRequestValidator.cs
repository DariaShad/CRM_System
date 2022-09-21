using FluentValidation;
using IncredibleBackendContracts.Requests;

namespace CRM_System.BusinessLayer;

public class TransferTransactionRequestValidator : AbstractValidator<TransactionTransferRequest>
{
    public TransferTransactionRequestValidator()
    {
        RuleFor(v => v.RecipientAccountId)
            .NotEmpty()
            .WithMessage("Fill in the field")
            .GreaterThan(0);

        RuleFor(v => v.RecipientAccountId)
            .NotEmpty()
            .WithMessage("Fill in the field")
            .GreaterThan(0);

        RuleFor(v => v.Amount)
            .NotEmpty()
            .WithMessage("Fill in the field")
            .GreaterThan(0)
            .WithMessage("Amount must be greater than zero");

        RuleFor(v => v.Currency)
            .NotEmpty()
            .WithMessage("Fill in the field")
            .IsInEnum()
            .WithMessage("Invalid TradingCurrency");
    }
}

