using Microsoft.Extensions.DependencyInjection;
using Moq;
using Onebrb.Core.Domain.Profile;
using Onebrb.Core.Interfaces;
using Onebrb.Core.Services.Profiles;

namespace Onebrb.API.Tests
{
    internal class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IProfileService, ProfileService>(x =>
            {
                var mockRepo = new Mock<IGenericRepository<Profile>>();
                var mockUoW = new Mock<IUnitOfWork>();
                return new ProfileService(mockRepo.Object, mockUoW.Object);
            });

            //services.AddTransient<ICommentService, CommentService>(x =>
            //{
            //    var mockRepo = new Mock<IGenericRepository<Comment>>();
            //    var mockUoW = new Mock<IUnitOfWork>();
            //    return new CommentService(mockRepo.Object);
            //});
        }
    }
}
