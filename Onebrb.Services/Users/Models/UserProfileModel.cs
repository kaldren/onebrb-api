namespace Onebrb.Application.Users.Models
{
    public class UserProfileModel
    {
        /// <summary>
        /// Gets or sets the profile Id
        /// </summary>
        public string ProfileId { get; set; }

        /// <summary>
        /// Gets or sets the profile first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the profile last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets short information about the services the person provides
        /// </summary>
        public string About { get; set; }
    }
}
