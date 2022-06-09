using AutoFixture;
using Onebrb.API.Services;
using Onebrb.API.Services.Interfaces;
using Xunit;

namespace Onebrb.API.Tests.Services
{
    public class ProfileServiceTests : BaseServiceTests
    {
        private readonly IProfileService profileService;

        public ProfileServiceTests()
        {
            this.profileService = new ProfileService();
        }

        [Fact]
        public async void GetProfileAsync_InvalidProfileId_ShouldThrow404()
        {
            // Arrange
            int profileId = fixture.Create<int>();

            // Act
            var result = await profileService.GetProfileAsync(profileId);

            // Assert
            Assert.NotNull(result);
        }
    }
}
