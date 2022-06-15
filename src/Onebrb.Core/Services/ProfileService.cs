using Onebrb.Core.Domain.Profile;
using Onebrb.Core.Interfaces;

namespace Onebrb.Core.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IGenericRepository<Profile> profileRepository;

        public ProfileService(IGenericRepository<Profile> profileRepository)
        {
            this.profileRepository = profileRepository;
        }

        public async Task<Profile?> GetProfileAsync(long id)
        {
            var profile = await profileRepository.GetByIdAsync(id);
            return null;
        }
    }
}
