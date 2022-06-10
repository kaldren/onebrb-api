using Onebrb.Core.Models;

namespace Onebrb.Core.Services.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileModel> GetProfileAsync(int id);
    }
}
