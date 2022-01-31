using ContactApplication.Application.Models.Contacts;
using ContactApplication.Application.Services.Contacts;
using ContactApplication.Domain.AppFlowControl;
using ContactApplication.Domain.Entities;
using ContactApplication.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactApplication.Application.Services.Suppliers
{
    public class ContactService : IContactService
    {
        private const string EXEPTION_COMUNICATION_ERROR = "Ocorreu um Erro na Comunicação! ";

        private readonly IContactRepository _repository;

        public ContactService(IContactRepository contactRepository)
        {
            _repository = contactRepository;
        }

        public async Task<ContactResponseModel> Create(ContactRequestModel contactRequestModel)
        {
            try
            {
                var contact = new Contact(
                    contactRequestModel.Name,
                    contactRequestModel.Company,
                    contactRequestModel.Email,
                    contactRequestModel.Telephone,
                    contactRequestModel.ContactTelephone);

                contact.Validate();
                await _repository.Create(contact);
                var contactResponse = CreateResponse(contact);
                return contactResponse;
            }
            catch (Exception ex)
            {
                throw new ServicesException(EXEPTION_COMUNICATION_ERROR, ex);
            }
        }

        public async Task<ContactResponseModel> Update(int id, ContactRequestModel contactRequestModel)
        {
            try
            {

                Contact contact = await ContactGet(id);

                contact.Update(
                contactRequestModel.Name,
                    contactRequestModel.Company,
                    contactRequestModel.Email,
                    contactRequestModel.Telephone,
                    contactRequestModel.ContactTelephone);

                contact.Validate();

                await _repository.Update(contact);

                return CreateResponse(contact);
            }
            catch (Exception ex)
            {
                throw new ServicesException(EXEPTION_COMUNICATION_ERROR, ex);
            }
        }

        public async Task<ContactResponseModel> Get(int id)
        {
            try
            {
                var contact = await ContactGet(id);
                var contactResponse = CreateResponse(contact);
                return contactResponse;
            }
            catch (Exception ex)
            {
                throw new ServicesException(EXEPTION_COMUNICATION_ERROR, ex);
            }
        }

        public async Task<IEnumerable<ContactResponseModel>> GetAll()
        {
            try
            {
                var contacts = await _repository.GetAll();

                return contacts.Select(contact => CreateResponse(contact));
            }
            catch (Exception ex)
            {
                throw new ServicesException(EXEPTION_COMUNICATION_ERROR, ex);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var contact = await ContactGet(id);
                await _repository.Delete(contact);
            }
            catch (Exception ex)
            {
                throw new ServicesException(EXEPTION_COMUNICATION_ERROR, ex);
            }
        }

        private static ContactResponseModel CreateResponse(Contact contact)
        {
            return new ContactResponseModel()
            {
                Id = contact.Id,
                Name = contact.Name,
                Company = contact.Company,
                Email = contact.Email,
                ContactTelephone = contact.ContactTelephone,
                Telephone = contact.Telephone,
                Deleted = contact.Deleted
            };
        }

        private async Task<Contact> ContactGet(int id)
        {
            var contact = await _repository.Get(id);
            if (contact == null)
            {
                throw new ServicesException("Não existe um contato com esse id");
            }

            if (contact.Deleted)
            {
                throw new ServicesException("Esse contato foi inativado,favor escolher um ativo");
            }
            return contact;
        }
    }
}
