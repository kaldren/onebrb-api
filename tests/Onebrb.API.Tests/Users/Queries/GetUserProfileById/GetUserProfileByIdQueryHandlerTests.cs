using AutoMapper;
using Moq;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Users.Models;
using Onebrb.Application.Users.Queries.GetUserProfileById;

namespace Onebrb.Application.UnitTests.Users.Queries.GetUserProfileById;

public class GetUserProfileByIdQueryHandlerTests
{
    private readonly Mock<IGenericRepository<Domain.Entities.Profile.Profile>> _mockRepo = new();
    private Fixture _fixture = new();
    private readonly Mock<IMapper> _mapper;

    public GetUserProfileByIdQueryHandlerTests()
    {
        _mapper = new Mock<IMapper>();
    }

    [Fact]
    public async Task GetUserProfileByIdQueryHandler_ValidUserId_ShouldReturnProfile()
    {
        string fakeUserId = _fixture.Create<string>();
        UserProfileModel fakeProfileModel = _fixture.Create<UserProfileModel>();

        Domain.Entities.Profile.Profile fakeProfile = _fixture.Build<Domain.Entities.Profile.Profile>()
            .Without(p => p.Comments)
            .Create();

        _mockRepo.Setup(p => p.GetAsync(It.Is<string>(p => p == fakeUserId))).ReturnsAsync(() => fakeProfile);

        _mapper.Setup(p => p.Map<UserProfileModel>(It.IsAny<Domain.Entities.Profile.Profile>()))
            .Returns(fakeProfileModel);

        var sut = new GetUserProfileByIdQueryHandler(_mockRepo.Object, _mapper.Object);

        var result = await sut.Handle(new GetUserProfileByIdQuery { Id = fakeUserId }, CancellationToken.None);

        Assert.NotNull(result);
    }
}
