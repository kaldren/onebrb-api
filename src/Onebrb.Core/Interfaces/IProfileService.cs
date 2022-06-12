using Onebrb.Core.Domain.Profile;

namespace Onebrb.Core.Interfaces
{
    public interface IProfileService
    {
        Task<Profile> GetProfileAsync(int id);
    }
}
