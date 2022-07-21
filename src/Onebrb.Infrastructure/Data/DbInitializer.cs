using Onebrb.Domain.Entities.Country;
using Onebrb.Domain.Entities.Profile;

namespace Onebrb.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(OnebrbDbContext onebrbDbContext)
        {
            if (onebrbDbContext.Profiles.Any())
            {
                return;
            }

            // Add profiles
            var profiles = new Profile[]
            {
                new Profile {Id = Guid.NewGuid().ToString(), FirstName = "Kaloyan", LastName="Drenski", Email = "drenski666@gmail.com", About = "Number one software developer in Veliko Varnovo", Phone = "0888 778877"},
                new Profile {Id = Guid.NewGuid().ToString(), FirstName = "Bob", LastName="Doe", Email = "bob@example.com", About = "Number one real estate broker in Veliko Varnovo", Phone = "0888 222222"},
                new Profile {Id = Guid.NewGuid().ToString(), FirstName = "Steve", LastName="Doe", Email = "steve@example.com", About = "Number one taxi driver in Veliko Varnovo", Phone = "0888 333333"},
                new Profile {Id = Guid.NewGuid().ToString(), FirstName = "Garry", LastName="Doe", Email = "garry@example.com", About = "Number one zumba trainer in Veliko Varnovo", Phone = "0888 444444"},
                new Profile {Id = Guid.NewGuid().ToString(), FirstName = "Michael", LastName="Doe", Email = "michael@example.com", About = "Number one hair stylist in Veliko Varnovo", Phone = "0888 555555"}
            };

            onebrbDbContext.Profiles.AddRange(profiles);

            // Add countries & cities
            var countries = new Country[]
            {
                new Country {
                    Id = Guid.NewGuid().ToString(),
                    Name="Bulgaria",
                    Cities = new City[] {
                        new City {Id = Guid.NewGuid().ToString(), Name = "Veliko Tarnovo"},
                        new City {Id = Guid.NewGuid().ToString(), Name = "Sofia"},
                        new City {Id = Guid.NewGuid().ToString(), Name = "Varna"},
                        new City {Id = Guid.NewGuid().ToString(), Name = "Bourgas"},
                    }
                },
                new Country {
                    Id = Guid.NewGuid().ToString(),
                    Name="USA",
                    Cities = new City[] {
                        new City {Id = Guid.NewGuid().ToString(), Name = "Los Angeles"},
                        new City {Id = Guid.NewGuid().ToString(), Name = "Las Vegas"},
                        new City {Id = Guid.NewGuid().ToString(), Name = "New York City"},
                        new City {Id = Guid.NewGuid().ToString(), Name = "Chicago"},
                    }
                },
                new Country {
                    Id = Guid.NewGuid().ToString(),
                    Name="Germany",
                    Cities = new City[] {
                        new City {Id = Guid.NewGuid().ToString(), Name = "Munich"},
                        new City {Id = Guid.NewGuid().ToString(), Name = "Berlin"},
                        new City {Id = Guid.NewGuid().ToString(), Name = "Frankfurt"},
                        new City {Id = Guid.NewGuid().ToString(), Name = "Hamburg"},
                    }
                },
            };

            onebrbDbContext.Countries.AddRange(countries);


            // Add comments
            var comments = new Comment[]
            {
                new Comment {Id = Guid.NewGuid().ToString(), Author =  profiles[1], Content = "This guy is amazing C# developer.", Date = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-1)), Recipient = profiles[0]},
                new Comment {Id = Guid.NewGuid().ToString(), Author =  profiles[2], Content = "This guy is amazing C# developer.", Date = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-2)), Recipient = profiles[0]},
                new Comment {Id = Guid.NewGuid().ToString(), Author =  profiles[3], Content = "This guy is amazing C# developer.", Date = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-2).AddHours(-4)), Recipient = profiles[0]},
            };

            onebrbDbContext.Comments.AddRange(comments);

            onebrbDbContext.SaveChanges();
        }
    }
}
