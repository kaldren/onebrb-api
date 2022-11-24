using AutoMapper;
using Moq;
using Onebrb.Application.Comments.Models;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Users.Queries.GetAllCommentsByUserId;
using Onebrb.Domain.Entities.UserAggregate;
using System.Linq.Expressions;

namespace Onebrb.Application.UnitTests.Users.Queries.GetAllCommentsByUserId
{
    public class GetAllCommentsByUserIdQueryHandlerTests
    {
        private readonly Mock<IGenericRepository<CommentEntity>> _mockRepo = new();
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

            CommentEntity fakeComment1 = _fixture.Build<CommentEntity>()
                .Without(p => p.Recipient)
                .Without(p => p.Author)
                .Create();

            CommentEntity fakeComment2 = _fixture.Build<CommentEntity>()
                .Without(p => p.Recipient)
                .Without(p => p.Author)
                .Create();

            ICollection<CommentEntity> fakeComments = new List<CommentEntity>()
            {
                fakeComment1,
                fakeComment2
            };

            ICollection<CommentModel> fakeReturnComment = _fixture.Create<ICollection<CommentModel>>();

            _mapper.Setup(p => p.Map<ICollection<CommentModel>>(It.IsAny<ICollection<CommentEntity>>()))
                .Returns(fakeReturnComment);

            var sut = new GetAllCommentsByUserIdQueryHandler(_mockRepo.Object, _mapper.Object);

            _mockRepo.Setup(p => p.GetAsync(It.IsAny<Expression<Func<CommentEntity, bool>>>()))
                .ReturnsAsync(() => fakeComments);

            var result = await sut.Handle(new GetAllCommentsByUserIdQuery { Id = fakeUserId }, CancellationToken.None);

            Assert.NotNull(result);
        }
    }
}
