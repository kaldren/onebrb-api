using Microsoft.Extensions.DependencyInjection;
using Moq;
using Onebrb.Core.Domain.Profile;
using Onebrb.Core.Interfaces;
using Onebrb.Core.Services;

namespace Onebrb.API.Tests
{
    internal class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IProfileService, ProfileService>(x =>
            {
                var mock = new Mock<IGenericRepository<Profile>>();
                return new ProfileService(mock.Object);
            });
        }
    }
}
