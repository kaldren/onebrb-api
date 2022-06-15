using Onebrb.Core.Domain.Profile;

namespace Onebrb.Core.Interfaces
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
    }
}
