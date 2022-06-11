namespace Onebrb.Core.Domain
{
    public class Profile
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the profile name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
