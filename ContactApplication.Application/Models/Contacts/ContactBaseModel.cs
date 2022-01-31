
namespace ContactApplication.Application.Models.Contacts
{
    public abstract class ContactBaseModel
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string ContactTelephone { get; set; }
    }
}
