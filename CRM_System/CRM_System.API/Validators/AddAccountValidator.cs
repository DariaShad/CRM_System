using CRM_System.API.Models.Requests;
using FluentValidation;
using System.Text.RegularExpressions;
namespace CRM_System.API.Validators
{
    public class AddAccountValidator : AbstractValidator<AddAccountRequest>
    {
        public AddAccountValidator()
        {
            RuleFor(a => a.TradingCurrency)
                .IsInEnum()
                .NotEmpty();

            RuleFor(a => a.Status)
                .IsInEnum()
                .NotEmpty();

            RuleFor(a => a.LeadId)
                .NotNull()
                .NotEmpty();
        }
    }
}
