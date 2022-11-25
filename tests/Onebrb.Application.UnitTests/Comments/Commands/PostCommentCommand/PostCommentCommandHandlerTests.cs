using AutoMapper;
using Moq;
using Onebrb.Application.Comments.Commands.PostSingleCommentCommand;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.UserAggregate;
using System.Linq.Expressions;

namespace Onebrb.Application.UnitTests.Comments.Commands.PostCommentCommand;
public class PostCommentCommandHandlerTests
{
    private readonly Mock<IGenericRepository<CommentEntity>> _mockRepo = new();
    private readonly Mock<IUnitOfWork> _mockUoW = new();
    private Fixture _fixture = new();
    private readonly IMapper _mapper;

    public PostCommentCommandHandlerTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CommentEntity, CommentModel>();
        });

        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task PostCommentCommandHandler_ValidComment_ShouldReturnCreatedComment()
    {
        // Arrange
        string fakeTrackId = Guid.NewGuid().ToString();

        CommentEntity fakeComment = _fixture.Build<CommentEntity>()
            .Without(p => p.Recipient)
            .Without(p => p.Author)
            .Create();

        PostCommentModel fakeCommentModel = _fixture.Create<PostCommentModel>();

        fakeComment.TrackId = fakeTrackId;

        _mockUoW.Setup(p => p.SaveChangesAsync())
            .ReturnsAsync(() => 1);


        _mockRepo.Setup(p => p.GetSingleOrDefault(It.IsAny<Expression<Func<CommentEntity, bool>>>())).ReturnsAsync(() => fakeComment);

        // Act
        var sut = new PostCommentCommandHandler(_mockRepo.Object, _mockUoW.Object, _mapper);

        CommentModel? result = await sut.Handle(new PostSingleCommentCommand { Comment = fakeCommentModel }, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }
}
