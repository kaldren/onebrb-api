﻿using Microsoft.EntityFrameworkCore;
using Onebrb.Core.Domain.Country;
using Onebrb.Core.Domain.Profile;

namespace Onebrb.Infrastructure
{
    public class OnebrbDbContext : DbContext
    {
        public OnebrbDbContext(DbContextOptions<OnebrbDbContext> options) : base(options)
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Profile>()
                .ToTable("Profiles");

            modelBuilder
                .Entity<City>()
                .ToTable("Cities")
                .HasOne(p => p.Country)
                .WithMany(p => p.Cities);

            modelBuilder
                .Entity<Country>()
                .ToTable("Countries")
                .HasMany(p => p.Cities);
        }
    }
}
