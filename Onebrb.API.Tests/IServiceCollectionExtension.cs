using Microsoft.Extensions.DependencyInjection;
using Onebrb.API.Services;
using Onebrb.API.Services.Interfaces;

namespace Onebrb.API.Tests
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddUnitTestsServices(this IServiceCollection services)
        {
            services.AddScoped<IProfileService, ProfileService>();
            return services;
        }
    }
}
