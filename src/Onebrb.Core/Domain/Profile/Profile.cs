namespace Onebrb.Core.Domain.Profile
{
    public class Profile : BaseEntity
    {
        /// <summary>
        /// Gets or sets the profile first name
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the profile last name
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets short information about the services the person provides
        /// </summary>
        public string About { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the comments
        /// </summary>
        public ICollection<Comment> Comments { get; set; }
    }
}
