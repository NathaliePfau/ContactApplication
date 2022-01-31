namespace ContactApplication.Application.Models.Contacts
{
    public class ContactResponseModel : ContactBaseModel
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
}
