using AutoMapper;
using Moq;
using Onebrb.Application.Interfaces;

namespace Onebrb.Application.UnitTests.Users.Queries.GetUserProfileById;

public class GetUserProfileByIdQueryHandlerTests
{
    private readonly Mock<IGenericRepository<Domain.Entities.UserAggregate.UserEntity>> _mockRepo = new();
    private Fixture _fixture = new();
    private readonly Mock<IMapper> _mapper;

    public GetUserProfileByIdQueryHandlerTests()
    {
        _mapper = new Mock<IMapper>();
    }

    [Fact]
    public async Task GetUserProfileByIdQueryHandler_ValidUserId_ShouldReturnProfile()
    {
        ////This code is needed to support recursion
        //_fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
        //.ForEach(b => _fixture.Behaviors.Remove(b));
        //_fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

        //string fakeUserId = _fixture.Create<string>();
        //UserProfileModel fakeProfileModel = _fixture.Create<UserProfileModel>();

        //Domain.Entities.ProfileAggregate.ProfileEntity fakeProfile = _fixture.Build<Domain.Entities.ProfileAggregate.ProfileEntity>()
        //    .Without(p => p.Comments)
        //    .Without(p => p.City)
        //    .Create();

        //_mockRepo.Setup(p => p.GetAsync(It.Is<string>(p => p == fakeUserId))).ReturnsAsync(() => fakeProfile);

        //_mapper.Setup(p => p.Map<UserProfileModel>(It.IsAny<Domain.Entities.ProfileAggregate.ProfileEntity>()))
        //    .Returns(fakeProfileModel);

        //var sut = new GetUserProfileByIdQueryHandler(_mockRepo.Object, _mapper.Object);

        //var result = await sut.Handle(new GetUserProfileByIdQuery { Id = fakeUserId }, CancellationToken.None);

        //Assert.NotNull(result);
    }
}
