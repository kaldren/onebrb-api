using Onebrb.Core.Domain.Profile;

namespace Onebrb.Core.Interfaces
{
    public interface IProfileService
    {
        /// <summary>
        /// Gets profile
        /// </summary>
        /// <param name="id">Profile identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the profile entity
        /// </returns>
        Task<Profile> GetProfileAsync(int id);
    }
}
