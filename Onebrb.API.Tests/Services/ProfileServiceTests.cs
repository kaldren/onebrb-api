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
