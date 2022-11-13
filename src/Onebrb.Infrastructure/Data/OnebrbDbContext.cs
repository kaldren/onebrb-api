using Microsoft.EntityFrameworkCore;
using Onebrb.Domain.Entities.CountryAggregate;
using Onebrb.Domain.Entities.ProfileAggregate;

namespace Onebrb.Infrastructure.Data
{
    public class OnebrbDbContext : DbContext
    {
        public OnebrbDbContext(DbContextOptions<OnebrbDbContext> options) : base(options)
        {
        }

        public DbSet<ProfileEntity> Profiles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ProfileEntity>()
                .ToTable("Profiles");

            modelBuilder
                .Entity<City>()
                .ToTable("Cities")
                .HasOne(p => p.Country)
                .WithMany(p => p.Cities)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder
                .Entity<Country>()
                .ToTable("Countries")
                .HasMany(p => p.Cities);

            modelBuilder
                .Entity<CommentEntity>()
                .ToTable("Comments")
                .HasOne(p => p.Author)
                .WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder
                .Entity<RatingEntity>()
                .ToTable("Ratings");
        }
    }
}
