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
        public async void GetProfileAsync_InvalidProfileId_Throws404NotFoundException()
        {
            // Arrange
            int profileId = fixture.Create<int>();

            // Act
            Task result() => profileService.GetProfileAsync(profileId);

            // Assert
            //Assert.ThrowsAsync<NotFound404Exception>;
        }
    }
}
