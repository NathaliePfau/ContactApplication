using ContactApplication.Domain.Entities;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ContactApplication.Domain.Validations
{
    public class ContactValidations : AbstractValidator<Contact>
    {
        public ContactValidations()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(f => f.Name)
                .NotEmpty().WithMessage("Nome não pode ser nulo")
                .Length(3, 250).WithMessage("Nome deve conter entre 3 e 250 caracteres.");

            RuleFor(f => f.Email)
                .MaximumLength(250).WithMessage("Email deve conter no maximo 250 caracteres.")
                .EmailAddress().WithMessage("Email inváldo!");

            RuleFor(f => f.Telephone)
                .NotEmpty().WithMessage("O telefone não pode ser nulo")
                .Must(IsValidPhoneNumber).WithMessage("O telefone é inválido ou possui caracteres incorretos!");

            RuleFor(f => f.ContactTelephone)
                .NotEmpty().WithMessage("O telefone comercial não pode ser nulo")
                .Must(IsValidPhoneNumber).WithMessage("O telefone comercial é inválido ou possui caracteres incorretos!");
        }

        private static bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.Match(phoneNumber,
                @"^(\(?[0-9]{2}\)?)\s?[0-9]{4,5}-?[0-9]{4}$").Success;
        }
    }
}
