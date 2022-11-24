using Microsoft.EntityFrameworkCore;
using Onebrb.Domain.Entities;
using Onebrb.Domain.Entities.CountryAggregate;
using Onebrb.Domain.Entities.UserAggregate;

namespace Onebrb.Infrastructure.Data
{
    public class OnebrbDbContext : DbContext
    {
        public OnebrbDbContext(DbContextOptions<OnebrbDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<OccupationEntity> Occupations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<UserEntity>()
                .ToTable("Users")
                .HasOne(p => p.Occupation);

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
                .ToTable("Ratings")
                .HasOne(p => p.Author)
                .WithMany(p => p.Ratings)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder
                .Entity<OccupationEntity>()
                .ToTable("Occupations");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var insertedEntries = this.ChangeTracker.Entries()
                                   .Where(x => x.State == EntityState.Added)
                                   .Select(x => x.Entity);

            foreach (var insertedEntry in insertedEntries)
            {
                var auditableEntity = insertedEntry as BaseEntity;
                //If the inserted object is an Auditable. 
                if (auditableEntity != null)
                {
                    auditableEntity.DateCreated = DateTimeOffset.UtcNow;
                }
            }

            var modifiedEntries = this.ChangeTracker.Entries()
                       .Where(x => x.State == EntityState.Modified)
                       .Select(x => x.Entity);

            foreach (var modifiedEntry in modifiedEntries)
            {
                //If the inserted object is an Auditable. 
                var auditableEntity = modifiedEntry as BaseEntity;
                if (auditableEntity != null)
                {
                    auditableEntity.DateUpdated = DateTimeOffset.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
