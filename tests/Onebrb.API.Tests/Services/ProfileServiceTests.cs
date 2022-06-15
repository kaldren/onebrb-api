using Onebrb.Core.Domain.Profile;
using Onebrb.Core.Interfaces;

namespace Onebrb.API.Tests.Services
{
    public class ProfileServiceTests : BaseServiceTests
    {
        private readonly IProfileService profileService;

        public ProfileServiceTests(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [Fact]
        public async void GetProfileAsync_ProfileIdNotExists_ReturnsNull()
        {
            // Arrange
            int profileId = fixture.Create<int>();

            // Act
            Profile? response = await profileService.GetProfileAsync(profileId);

            // Assert
            Assert.Null(response);
        }
    }
}
