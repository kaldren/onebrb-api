using Microsoft.EntityFrameworkCore;
using Onebrb.Core.Domain.Profile;

namespace Onebrb.Infrastructure
{
    public class OnebrbDbContext : DbContext
    {
        public OnebrbDbContext(DbContextOptions<OnebrbDbContext> options) : base(options)
        {
        }

        public DbSet<Profile> Profiles { get; set; }
    }
}
