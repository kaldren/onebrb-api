using Moq;
using Onebrb.Application.Comments;
using Onebrb.Domain.Entities.Profile;
using Onebrb.Domain.Interfaces;
using System.Linq.Expressions;

namespace Onebrb.Application.UnitTests.Services.Comments
{
    public class CommentServiceTests : BaseServiceTests
    {
        private readonly ICommentService _commentService;
        private readonly Mock<IGenericRepository<Comment>> _mockCommentsRepo;

        public CommentServiceTests()
        {
            _mockCommentsRepo = new();
            _commentService = new CommentService(_mockCommentsRepo.Object);
        }

        [Fact]
        public async void GetCommentAsync_CommentFound_ReturnsComment()
        {
            // Arrange
            long profileId = fixture.Create<long>();
            Comment fakeComment = fixture.Build<Comment>()
                .Without(p => p.Recipient)
                .Without(p => p.Author)
                .Create();

            _mockCommentsRepo.Setup(p => p.GetAsync(profileId))
                .ReturnsAsync(() => fakeComment);

            // Act
            Comment? res = await _commentService.GetCommentAsync(profileId);

            // Assert
            Assert.NotNull(res);
        }

        [Fact]
        public async void GetCommentAsync_CommentNotFound_ReturnsNull()
        {
            // Arrange
            long profileId = fixture.Create<long>();

            _mockCommentsRepo.Setup(p => p.GetAsync(profileId))
                .ReturnsAsync(() => null);

            // Act
            Comment? res = await _commentService.GetCommentAsync(profileId);

            // Assert
            Assert.Null(res);
        }

        [Fact]
        public async void GetCommentsAsync_CommentsFound_ReturnsComments()
        {
            // Arrange
            long fakeRecipientId = fixture.Create<long>();

            Comment fakeComment1 = fixture.Build<Comment>()
                .Without(p => p.Recipient)
                .Without(p => p.Author)
                .Create();

            Comment fakeComment2 = fixture.Build<Comment>()
                .Without(p => p.Recipient)
                .Without(p => p.Author)
                .Create();

            ICollection<Comment> fakeComments = new List<Comment>()
            {
                fakeComment1,
                fakeComment2
            };

            int expectedCommentsCount = 2;

            _mockCommentsRepo.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Comment, bool>>>()))
                .ReturnsAsync(() => fakeComments);

            // Act
            ICollection<Comment> res = await _commentService.GetCommentsAsync(fakeRecipientId);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(res.Count, expectedCommentsCount);
        }

        [Fact]
        public async void GetCommentsAsync_CommentsNotFound_ReturnsEmptyCollection()
        {
            // Arrange
            long fakeRecipientId = fixture.Create<long>();

            ICollection<Comment> fakeComments = new List<Comment>();

            int expectedCommentsCount = 0;

            _mockCommentsRepo.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Comment, bool>>>()))
                .ReturnsAsync(() => fakeComments);

            // Act
            ICollection<Comment> res = await _commentService.GetCommentsAsync(fakeRecipientId);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(res.Count, expectedCommentsCount);
        }
    }
}
