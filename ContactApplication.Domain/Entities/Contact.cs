using ContactApplication.Domain.Validations;
using FluentValidation;
using System.Collections.Generic;

namespace ContactApplication.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public string Name { get; protected set; }
        public string Company { get; protected set; }
        public string Email { get; protected set; }
        public string Telephone { get; protected set; }
        public string ContactTelephone { get; protected set; }

        protected Contact() { }

        public Contact(string name, string company, string email, string telephone, string contactTelephone)
        {
            Name = name;
            Company = company;
            Email = email;
            Telephone = telephone;
            ContactTelephone = contactTelephone;
        }

        public void Update(string name, string company, string email, string telephone, string contactTelephone)
        {
            Name = name;
            Company = company;
            Email = email;
            Telephone = telephone;
            ContactTelephone = contactTelephone;
        }

        public void Validate()
        {
            var validate = new ContactValidations();
            validate.ValidateAndThrow(this);
        }
    }
}
