﻿using FluentValidation;
using System.Text.RegularExpressions;

namespace CRM_System.API;

public class LeadRegistrationValidator : AbstractValidator<LeadRegistrationRequest>
{
    public LeadRegistrationValidator()
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
        //здесь же ещё нужен кастомный валидатор, верно?

        RuleFor(v => v.Email)
            .NotEmpty()
            .WithMessage("Fill in the field")
            .EmailAddress()
            .WithMessage("Invalid email");

        RuleFor(v => v.Phone)
             .NotEmpty()
             .WithMessage("Fill in the field")
             .Matches(new Regex(@"^((8 |\+7)[\- ] ?) ? (\(?\d{ 3}\)?[\- ]?)?[\d\- ]{ 7,10}$"))
             .WithMessage("Invalid phone number");

        RuleFor(v => v.Passport)
             .NotEmpty()
             .WithMessage("Fill in the field")
             .MinimumLength(11)
             .WithMessage("Fill in number and series of passport at least")
             .MaximumLength(73)
             .WithMessage("Maximum length is 73 symbols");

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

        RuleFor(v => v.Password)
              .NotEmpty()
              .WithMessage("Fill in the field")
              .MinimumLength(8)
              .WithMessage("Minimum length is 8 symbols")
              .MaximumLength(25)
              .WithMessage("Maximum length is 25 symbols");
            //добавить кастомный валидатор для более сложного пароля?
    }
}