using Onebrb.Core.Entities.Profile;

namespace Onebrb.Services.Profiles
{
    public interface IProfileService
    {
        Task<Profile> GetProfileAsync(int id);
    }
}
