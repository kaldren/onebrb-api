using AutoMapper;
using Moq;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Comments.Queries.GetSingleComment;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.Profile;

namespace Onebrb.Application.UnitTests.Comments.Queries.GetSingleComment;

public class GetSingleCommentByCommentIdQueryHandlerTests
{
    private readonly Mock<IGenericRepository<Comment>> _mockRepo = new();
    private Fixture _fixture = new();
    private readonly Mock<IMapper> _mapper;

    public GetSingleCommentByCommentIdQueryHandlerTests()
    {
        _mapper = new Mock<IMapper>();
    }

    [Fact]
    public async Task GetSingleCommentByCommentIdQueryHandler_ValidId_ShouldReturnSingleComment()
    {
        long fakeId = 1;

        Comment fakeComment = _fixture.Build<Comment>()
            .Without(p => p.Recipient)
            .Without(p => p.Author)
            .Create();

        CommentModel fakeReturnComment = _fixture.Create<CommentModel>();

        _mapper.Setup(p => p.Map<CommentModel>(It.IsAny<Comment>()))
            .Returns(fakeReturnComment);

        var sut = new GetSingleCommentByCommentIdQueryHandler(_mockRepo.Object, _mapper.Object);

        _mockRepo.Setup(p => p.GetAsync(It.Is<long>(p => p == fakeId))).ReturnsAsync(() => fakeComment);

        var result = await sut.Handle(new GetSingleCommentByCommentIdQuery { Id = 1 }, CancellationToken.None);

        Assert.NotNull(result);
    }
}
