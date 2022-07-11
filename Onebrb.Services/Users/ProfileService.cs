using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.Profile;

namespace Onebrb.Application.Profiles
{
    public class ProfileService : IProfileService
    {
        private readonly IGenericRepository<Profile> profileRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProfileService(
            IGenericRepository<Profile> profileRepository,
            IUnitOfWork unitOfWork)
        {
            this.profileRepository = profileRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Profile?> ActivateProfile()
        {
            return null;
            // Add it to the db
            //await profileRepository.Insert(new Profile());
        }

        public async Task<Profile?> GetProfileAsync(long id)
        {
            return await profileRepository.GetAsync(id);
        }

        public async Task<Profile?> GetProfileAsync(string email)
        {
            return await profileRepository.GetSingleOrDefault(p => p.Email == email);
        }

        public async Task<Profile?> UpdateProfileAsync(Profile profile)
        {
            profileRepository.Update(profile);

            int result = await unitOfWork.SaveChangesAsync();

            return result > 0 ? profile : null;
        }
    }
}
