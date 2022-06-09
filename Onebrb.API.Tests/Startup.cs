using Microsoft.Extensions.DependencyInjection;
using Onebrb.API.Services;

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
