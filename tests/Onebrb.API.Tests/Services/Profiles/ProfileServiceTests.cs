using Onebrb.Core.Domain.Profile;
using Onebrb.Core.Services.Profiles;

namespace Onebrb.API.Tests.Services.Profiles
{
    public class ProfileServiceTests : BaseServiceTests
    {
        private readonly IProfileService _profileService;

        public ProfileServiceTests(IProfileService profileService)
        {
            this._profileService = profileService;
        }

        [Fact]
        public async void GetProfileAsync_ProfileIdNotExists_ReturnsNull()
        {
            // Arrange
            int profileId = fixture.Create<int>();

            // Act
            Profile? response = await _profileService.GetProfileAsync(profileId);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async void ActivateProfileAsync_ProfileActivated_ReturnsProfile()
        {
            // Arrange & Act
            Profile? response = await _profileService.ActivateProfile();

            // Assert
            Assert.NotNull(response);
        }
    }
}
