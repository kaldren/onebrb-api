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
                new Profile {FirstName = "Kaloyan", LastName="Drenski", Email = "drenski666@gmail.com", About = "Number one software developer in Veliko Varnovo", Phone = "0888 778877"},
                new Profile {FirstName = "Bob", LastName="Doe", Email = "bob@example.com", About = "Number one real estate broker in Veliko Varnovo", Phone = "0888 222222"},
                new Profile {FirstName = "Steve", LastName="Doe", Email = "steve@example.com", About = "Number one taxi driver in Veliko Varnovo", Phone = "0888 333333"},
                new Profile {FirstName = "Garry", LastName="Doe", Email = "garry@example.com", About = "Number one zumba trainer in Veliko Varnovo", Phone = "0888 444444"},
                new Profile {FirstName = "Michael", LastName="Doe", Email = "michael@example.com", About = "Number one hair stylist in Veliko Varnovo", Phone = "0888 555555"}
            };

            onebrbDbContext.Profiles.AddRange(profiles);

            // Add countries & cities
            var countries = new Country[]
            {
                new Country {
                    Id = Guid.NewGuid().ToString(),
                    Name="Bulgaria",
                    Cities = new City[] {
                        new City {Name = "Veliko Tarnovo"},
                        new City {Name = "Sofia"},
                        new City {Name = "Varna"},
                        new City {Name = "Bourgas"},
                    }
                },
                new Country {
                    Id = Guid.NewGuid().ToString(),
                    Name="USA",
                    Cities = new City[] {
                        new City {Name = "Los Angeles"},
                        new City {Name = "Las Vegas"},
                        new City {Name = "New York City"},
                        new City {Name = "Chicago"},
                    }
                },
                new Country {
                    Id = Guid.NewGuid().ToString(),
                    Name="Germany",
                    Cities = new City[] {
                        new City {Name = "Munich"},
                        new City {Name = "Berlin"},
                        new City {Name = "Frankfurt"},
                        new City {Name = "Hamburg"},
                    }
                },
            };

            onebrbDbContext.Countries.AddRange(countries);


            // Add comments
            var comments = new Comment[]
            {
                new Comment {Author =  profiles[1], Content = "This guy is amazing C# developer.", Date = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-1)), Recipient = profiles[0]},
                new Comment {Author =  profiles[2], Content = "This guy is amazing C# developer.", Date = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-2)), Recipient = profiles[0]},
                new Comment {Author =  profiles[3], Content = "This guy is amazing C# developer.", Date = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-2).AddHours(-4)), Recipient = profiles[0]},
            };

            onebrbDbContext.Comments.AddRange(comments);

            onebrbDbContext.SaveChanges();
        }
    }
}
