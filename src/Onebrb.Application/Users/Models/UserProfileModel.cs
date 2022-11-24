namespace Onebrb.Application.Users.Models
{
    public class UserProfileModel
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
        /// Gets or sets the profile full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the email visibility
        /// </summary>
        public bool? IsEmailVisible { get; set; }

        // TODO: Phone validation - until then not showing phones
        ///// <summary>
        ///// Gets or sets the phone number
        ///// </summary>
        //public string? Phone { get; set; }

        ///// <summary>
        ///// Gets or sets the phone number visibility
        ///// </summary>
        //public bool? IsPhoneVisible { get; set; }

        /// <summary>
        /// Gets or sets short information about the services the person provides
        /// </summary>
        public string? About { get; set; }

        /// <summary>
        /// Gets or sets user profile photo
        /// </summary>
        public string? Photo { get; set; }

        /// <summary>
        /// Gets or sets the country
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// Gets or sets the city
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Gets or sets the user rating
        /// </summary>
        public double? Rating { get; set; }

        /// <summary>
        /// Gets or sets the user occupation
        /// </summary>
        public string? OccupationName { get; set; }

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
