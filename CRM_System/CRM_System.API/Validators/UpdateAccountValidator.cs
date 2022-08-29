using CRM_System.API.Models.Requests;
using FluentValidation;

namespace CRM_System.API.Validators
{
    public class UpdateAccountValidator : AbstractValidator<UpdateAccountRequest>
    {
        public UpdateAccountValidator()
        {
            RuleFor(a => a.Status)
                .IsInEnum()
                .NotEmpty();
        }
    }
}
