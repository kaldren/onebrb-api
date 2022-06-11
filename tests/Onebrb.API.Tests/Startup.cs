using Microsoft.Extensions.DependencyInjection;
using Onebrb.Core.Interfaces;
using Onebrb.Services.Profiles;

namespace Onebrb.API.Tests
{
    internal class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IProfileService, ProfileService>();
        }
    }
}
