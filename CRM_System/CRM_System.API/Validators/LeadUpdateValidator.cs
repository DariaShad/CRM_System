using FluentValidation;
using System.Text.RegularExpressions;

namespace CRM_System.API;

public class LeadUpdateValidator : AbstractValidator<LeadUpdateRequest>
{
    public LeadUpdateValidator()
    {
        RuleFor(v => v.FirstName)
            .NotEmpty()
            .WithMessage("Fill in the field")
            .MinimumLength(2)
            .WithMessage("Minimum length is 2 symbols")
            .MaximumLength(23)
            .WithMessage("Maximum length is 23 symbols");

        RuleFor(v => v.LastName)
            .NotEmpty()
            .WithMessage("Fill in the field")
            .MinimumLength(2)
            .WithMessage("Minimum length is 2 symbols")
            .MaximumLength(23)
            .WithMessage("Maximum length is 23 symbols");

        RuleFor(v => v.Patronymic)
            .NotEmpty()
            .WithMessage("Fill in the field")
            .MinimumLength(2)
            .WithMessage("Minimum length is 2 symbols")
            .MaximumLength(23)
            .WithMessage("Maximum length is 23 symbols");

        RuleFor(v => v.Birthday)
            .NotEmpty()
            .WithMessage("Fill in the field");

        RuleFor(v => v.Phone)
             .NotEmpty()
             .WithMessage("Fill in the field")
             .Matches(new Regex(@"^((8 |\+7)[\- ] ?) ? (\(?\d{ 3}\)?[\- ]?)?[\d\- ]{ 7,10}$"))
             .WithMessage("Invalid phone number");

        RuleFor(v => v.City)
             .NotEmpty()
             .WithMessage("Fill in the field")
             .IsInEnum()
             .WithMessage("Invalid city");

        RuleFor(v => v.Address)
              .NotEmpty()
              .WithMessage("Fill in the field")
              .MinimumLength(10)
              .WithMessage("Minimum length is 10 symbols")
              .MaximumLength(27)
              .WithMessage("Maximum length is 28 symbols");
    }
}
