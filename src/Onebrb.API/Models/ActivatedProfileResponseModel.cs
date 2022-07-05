namespace Onebrb.API.Models
{
    public class ActivatedProfileResponseModel
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string About { get; set; }

        public bool IsDeleted { get; set; }
    }
}
