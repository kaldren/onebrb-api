using Onebrb.Core.Domain.Profile;

namespace Onebrb.Core.Services.Interfaces
{
    public interface IProfileService
    {
        Task<Profile> GetProfileAsync(int id);
    }
}
