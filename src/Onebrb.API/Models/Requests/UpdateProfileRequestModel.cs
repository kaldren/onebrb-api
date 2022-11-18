namespace Onebrb.API.Models.Requests
{
    public class UpdateUserProfileRequestModel
    {
        /// <summary>
        /// Gets or sets the profile Id
        /// </summary>
        public string Id { get; set; }

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
        /// Gets or sets email visibility
        /// </summary>
        public bool IsEmailVisible { get; set; }

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets phone visibility
        /// </summary>
        public bool IsPhoneVisible { get; set; }

        /// <summary>
        /// Gets or sets short information about the services the person provides
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Gets or sets user's occupation
        /// </summary>
        public string OccupationName { get; set; }

        /// <summary>
        /// Gets or sets the linkedin profile
        /// </summary>
        public string? LinkedInProfile { get; set; }

        /// <summary>
        /// Gets or sets the facebook profile
        /// </summary>
        public string? FacebookProfile { get; set; }

        /// <summary>
        /// Gets or sets the instagram profile
        /// </summary>
        public string? InstagramProfile { get; set; }
    }
}
