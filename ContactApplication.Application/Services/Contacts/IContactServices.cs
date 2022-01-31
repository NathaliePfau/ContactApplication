using ContactApplication.Application.Models.Contacts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactApplication.Application.Services.Contacts
{
    public interface IContactService
    {
        Task<ContactResponseModel> Create(ContactRequestModel supplierRequestModel);
        Task<ContactResponseModel> Get(int id);
        Task<IEnumerable<ContactResponseModel>> GetAll();
        Task<ContactResponseModel> Update(int id, ContactRequestModel supplierRequestModel);
        Task Delete(int id);
    }
}
