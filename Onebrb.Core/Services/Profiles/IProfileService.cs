using Onebrb.Core.Domain.Profile;

namespace Onebrb.Core.Services.Profiles
{
    public interface IProfileService
    {
        Task<Profile> GetProfileAsync(int id);
    }
}
