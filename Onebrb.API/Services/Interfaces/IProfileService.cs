using Onebrb.API.Services.Models;

namespace Onebrb.API.Services.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileModel> GetProfileAsync(int id);
    }
}
