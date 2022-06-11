using Onebrb.Core.Domain;

namespace Onebrb.Core.Interfaces
{
    public interface IProfileService
    {
        Task<Profile> GetProfileAsync(int id);
    }
}
