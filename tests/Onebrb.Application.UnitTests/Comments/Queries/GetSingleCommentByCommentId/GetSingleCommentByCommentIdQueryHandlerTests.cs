using AutoMapper;
using Moq;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Comments.Queries.GetSingleCommentByCommentId;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.ProfileAggregate;

namespace Onebrb.Application.UnitTests.Comments.Queries.GetSingleCommentByCommentId;

public class GetSingleCommentByCommentIdQueryHandlerTests
{
    private readonly Mock<IGenericRepository<CommentEntity>> _mockRepo = new();
    private Fixture _fixture = new();
    private readonly Mock<IMapper> _mapper;

    public GetSingleCommentByCommentIdQueryHandlerTests()
    {
        _mapper = new Mock<IMapper>();
    }

    [Fact]
    public async Task GetSingleCommentByCommentIdQueryHandler_ValidId_ShouldReturnSingleComment()
    {
        string fakeId = _fixture.Create<string>();

        CommentEntity fakeComment = _fixture.Build<CommentEntity>()
            .Without(p => p.Recipient)
            .Without(p => p.Author)
            .Create();

        CommentModel fakeReturnComment = _fixture.Create<CommentModel>();

        _mapper.Setup(p => p.Map<CommentModel>(It.IsAny<CommentEntity>()))
            .Returns(fakeReturnComment);

        var sut = new GetSingleCommentByCommentIdQueryHandler(_mockRepo.Object, _mapper.Object);

        _mockRepo.Setup(p => p.GetAsync(It.Is<string>(p => p == fakeId))).ReturnsAsync(() => fakeComment);

        var result = await sut.Handle(new GetSingleCommentByCommentIdQuery { Id = fakeId }, CancellationToken.None);

        Assert.NotNull(result);
    }
}
