using Onebrb.Core.Domain.Profile;

namespace Onebrb.Core.Services.Profiles
{
    public interface IProfileService
    {
        /// <summary>
        /// Gets a profile entity by id.
        /// </summary>
        /// <param name="id">The id of the profile</param>
        /// <returns>
        /// Asynchronously returns the <typeparamref name="Profile" /> entity, or null if no entity is found.
        /// </returns>
        Task<Profile?> GetProfileAsync(long id);

        /// <summary>
        /// Gets a profile entity by email.
        /// </summary>
        /// <param name="id">The email of the profile</param>
        /// <returns>
        /// Asynchronously returns the <typeparamref name="Profile" /> entity, or null if no entity is found.
        /// </returns>
        Task<Profile?> GetProfileAsync(string email);

        /// <summary>
        /// Updates a profile entity by email.
        /// </summary>
        /// <param name="id">The email of the profile</param>
        /// <returns>
        /// Asynchronously returns the <typeparamref name="Profile" /> entity, or null if the entity was not updated.
        /// </returns>
        Task<Profile?> UpdateProfileAsync(Profile profile);

        /// <summary>
        /// Activates the profile of the user.
        /// </summary>
        /// <returns>
        /// Asynchronously returns the <typeparamref name="Profile" /> entity, or null if the entity was not created.
        /// </returns>
        Task<Profile?> ActivateProfile();
    }
}
