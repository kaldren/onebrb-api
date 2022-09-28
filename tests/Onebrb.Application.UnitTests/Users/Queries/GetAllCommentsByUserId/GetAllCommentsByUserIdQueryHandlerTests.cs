using AutoMapper;
using Moq;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Users.Queries.GetAllCommentsByUserId;
using Onebrb.Domain.Entities.Profile;
using System.Linq.Expressions;

namespace Onebrb.Application.UnitTests.Users.Queries.GetAllCommentsByUserId
{
    public class GetAllCommentsByUserIdQueryHandlerTests
    {
        private readonly Mock<IGenericRepository<Comment>> _mockRepo = new();
        private Fixture _fixture = new();
        private readonly Mock<IMapper> _mapper;

        public GetAllCommentsByUserIdQueryHandlerTests()
        {
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetAllCommentsByUserIdQueryHandler_ValidUserId_ShouldReturnAllComments()
        {
            string fakeUserId = _fixture.Create<string>();

            Comment fakeComment1 = _fixture.Build<Comment>()
                .Without(p => p.Recipient)
                .Without(p => p.Author)
                .Create();

            Comment fakeComment2 = _fixture.Build<Comment>()
                .Without(p => p.Recipient)
                .Without(p => p.Author)
                .Create();

            ICollection<Comment> fakeComments = new List<Comment>()
            {
                fakeComment1,
                fakeComment2
            };

            ICollection<CommentModel> fakeReturnComment = _fixture.Create<ICollection<CommentModel>>();

            _mapper.Setup(p => p.Map<ICollection<CommentModel>>(It.IsAny<ICollection<Comment>>()))
                .Returns(fakeReturnComment);

            var sut = new GetAllCommentsByUserIdQueryHandler(_mockRepo.Object, _mapper.Object);

            _mockRepo.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Comment, bool>>>()))
                .ReturnsAsync(() => fakeComments);

            var result = await sut.Handle(new GetAllCommentsByUserIdQuery { Id = fakeUserId }, CancellationToken.None);

            Assert.NotNull(result);
        }
    }
}
