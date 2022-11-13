using Onebrb.Domain.Entities.CountryAggregate;
using Onebrb.Domain.Entities.ProfileAggregate;

namespace Onebrb.Infrastructure.Data;

public static class DbInitializer
{
    public static void Initialize(OnebrbDbContext onebrbDbContext)
    {
        if (onebrbDbContext.Profiles.Any())
        {
            return;
        }

        string profile1Id = "0a945d97-4e71-4956-b5dd-c6706173dd36";
        string profile2Id = "4f170031-f087-4ad5-8343-7da89e8cb8be";

        // Countries
        string countryBGId = Guid.NewGuid().ToString();
        string countryUSAId = Guid.NewGuid().ToString();
        string countryDEId = Guid.NewGuid().ToString();

        // Cities
        string velikoTarnovoId = Guid.NewGuid().ToString();

        var countries = new Country[]
        {
            new Country {
                Id = countryBGId,
                Name="Bulgaria",
                Cities = new City[] {
                    new City {Id = velikoTarnovoId, Name = "Veliko Tarnovo"},
                    new City {Id = Guid.NewGuid().ToString(), Name = "Sofia"},
                    new City {Id = Guid.NewGuid().ToString(), Name = "Varna"},
                    new City {Id = Guid.NewGuid().ToString(), Name = "Bourgas"},
                }
            },
            new Country {
                Id = countryUSAId,
                Name="USA",
                Cities = new City[] {
                    new City {Id = Guid.NewGuid().ToString(), Name = "Los Angeles"},
                    new City {Id = Guid.NewGuid().ToString(), Name = "Las Vegas"},
                    new City {Id = Guid.NewGuid().ToString(), Name = "New York City"},
                    new City {Id = Guid.NewGuid().ToString(), Name = "Chicago"},
                }
            },
            new Country {
                Id = countryDEId,
                Name="Germany",
                Cities = new City[] {
                    new City {Id = Guid.NewGuid().ToString(), Name = "Munich"},
                    new City {Id = Guid.NewGuid().ToString(), Name = "Berlin"},
                    new City {Id = Guid.NewGuid().ToString(), Name = "Frankfurt"},
                    new City {Id = Guid.NewGuid().ToString(), Name = "Hamburg"},
                }
            },
        };

        // Add profiles
        var profiles = new ProfileEntity[]
        {
            new ProfileEntity {Id = profile1Id, FirstName = "Kaloyan", LastName="Drenski", Email = "drenski666@gmail.com", About = "Number one software developer in Veliko Varnovo", Phone = "0888 778877", CountryId = countryBGId, CityId = velikoTarnovoId},
            new ProfileEntity {Id = profile2Id, FirstName = "Bob", LastName="Doe", Email = "bob@example.com", About = "Number one real estate broker in Veliko Varnovo", Phone = "0888 222222", CountryId = countryBGId, CityId = velikoTarnovoId, IsEmailVisible = true},
            new ProfileEntity {Id = Guid.NewGuid().ToString(), FirstName = "Steve", LastName="Doe", Email = "steve@example.com", About = "Number one taxi driver in Veliko Varnovo", Phone = "0888 333333", CountryId = countryBGId, CityId = velikoTarnovoId},
            new ProfileEntity {Id = Guid.NewGuid().ToString(), FirstName = "Garry", LastName="Doe", Email = "garry@example.com", About = "Number one zumba trainer in Veliko Varnovo", Phone = "0888 444444", CountryId = countryBGId, CityId = velikoTarnovoId},
            new ProfileEntity {Id = Guid.NewGuid().ToString(), FirstName = "Michael", LastName="Doe", Email = "michael@example.com", About = "Number one hair stylist in Veliko Varnovo", Phone = "0888 555555", CountryId = countryBGId, CityId = velikoTarnovoId}
        };

        onebrbDbContext.Profiles.AddRange(profiles);

        // Add countries & cities
        onebrbDbContext.Countries.AddRange(countries);

        // Add ratings
        var ratings = new RatingEntity[]
        {
            new RatingEntity { Rating = 5, AuthorId = profiles[1].Id, RecipientId = profiles[0].Id},
            new RatingEntity { Rating = 3, AuthorId = profiles[2].Id, RecipientId = profiles[0].Id},
            new RatingEntity { Rating = 2, AuthorId = profiles[3].Id, RecipientId = profiles[0].Id},
        };

        // Add comments
        var comments = new CommentEntity[]
        {
            new CommentEntity {Id = Guid.NewGuid().ToString(), Author =  profiles[1], Content = "This guy is amazing C# developer.", Date = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-1)), Recipient = profiles[0]},
            new CommentEntity {Id = Guid.NewGuid().ToString(), Author =  profiles[2], Content = "This guy is amazing C# developer.", Date = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-2)), Recipient = profiles[0]},
            new CommentEntity {Id = Guid.NewGuid().ToString(), Author =  profiles[3], Content = "This guy is amazing C# developer.", Date = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-2).AddHours(-4)), Recipient = profiles[0]},
        };

        comments[0].Rating = ratings[0];
        comments[1].Rating = ratings[1];
        comments[2].Rating = ratings[2];

        onebrbDbContext.Comments.AddRange(comments);

        onebrbDbContext.SaveChanges();
    }
}
