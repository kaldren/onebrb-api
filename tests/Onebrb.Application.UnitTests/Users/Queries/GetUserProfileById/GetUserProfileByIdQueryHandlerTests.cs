using AutoMapper;
using Moq;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Users.Models;
using Onebrb.Application.Users.Queries.GetUserProfileById;

namespace Onebrb.Application.UnitTests.Users.Queries.GetUserProfileById;

public class GetUserProfileByIdQueryHandlerTests
{
    private readonly Mock<IGenericRepository<Domain.Entities.ProfileAggregate.Profile>> _mockRepo = new();
    private Fixture _fixture = new();
    private readonly Mock<IMapper> _mapper;

    public GetUserProfileByIdQueryHandlerTests()
    {
        _mapper = new Mock<IMapper>();
    }

    [Fact]
    public async Task GetUserProfileByIdQueryHandler_ValidUserId_ShouldReturnProfile()
    {
        //This code is needed to support recursion
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
        .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

        string fakeUserId = _fixture.Create<string>();
        UserProfileModel fakeProfileModel = _fixture.Create<UserProfileModel>();

        Domain.Entities.ProfileAggregate.Profile fakeProfile = _fixture.Build<Domain.Entities.ProfileAggregate.Profile>()
            .Without(p => p.Comments)
            .Without(p => p.City)
            .Create();

        _mockRepo.Setup(p => p.GetAsync(It.Is<string>(p => p == fakeUserId))).ReturnsAsync(() => fakeProfile);

        _mapper.Setup(p => p.Map<UserProfileModel>(It.IsAny<Domain.Entities.ProfileAggregate.Profile>()))
            .Returns(fakeProfileModel);

        var sut = new GetUserProfileByIdQueryHandler(_mockRepo.Object, _mapper.Object);

        var result = await sut.Handle(new GetUserProfileByIdQuery { Id = fakeUserId }, CancellationToken.None);

        Assert.NotNull(result);
    }
}
