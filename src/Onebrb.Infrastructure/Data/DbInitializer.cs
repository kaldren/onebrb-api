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

        // Occupations
        int counter = 0;
        var occupationsCount = File.ReadLines($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "occupations.txt")}").Count();
        var occupations = new OccupationEntity[occupationsCount];

        // Read the file and display it line by line.  
        foreach (string line in System.IO.File.ReadLines($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "occupations.txt")}"))
        {
            occupations[counter] = new OccupationEntity { Name = line };
            counter++;
        }

        onebrbDbContext.Occupations.AddRange(occupations);

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

        // Add social media profiles
        string facebookProfile = "kaloyan.drenski";
        string instagramProfile = "kaloyan.drenski";
        string linkedinProfile = "kdrenski";

        // Add profiles
        var profiles = new ProfileEntity[]
        {
            //new ProfileEntity {Id = profile1Id, FirstName = "Kaloyan", LastName="Drenski", Email = "drenski666@gmail.com", About = "Number one software developer in Veliko Varnovo", Phone = "0888 778877", CountryId = countryBGId, CityId = velikoTarnovoId, Occupation = occupations[15], LinkedInProfile = linkedinProfile, FacebookProfile = facebookProfile, InstagramProfile = instagramProfile},
            new ProfileEntity {Id = profile2Id, FirstName = "Bob", LastName="Doe", Email = "bob@example.com", About = "Number one real estate broker in Veliko Varnovo", Phone = "0888 222222", IsPhoneVisible = true, CountryId = countryBGId, CityId = velikoTarnovoId, Occupation = occupations[41]},
            new ProfileEntity {Id = Guid.NewGuid().ToString(), FirstName = "Steve", LastName="Doe", Email = "steve@example.com", About = "Number one taxi driver in Veliko Varnovo", Phone = "0888 333333", IsPhoneVisible = true, CountryId = countryBGId, CityId = velikoTarnovoId, Occupation = occupations[44]},
            new ProfileEntity {Id = Guid.NewGuid().ToString(), FirstName = "Garry", LastName="Doe", Email = "garry@example.com", About = "Number one zumba trainer in Veliko Varnovo", Phone = "0888 444444", IsPhoneVisible = true, CountryId = countryBGId, CityId = velikoTarnovoId, Occupation = occupations[55]},
            new ProfileEntity {Id = Guid.NewGuid().ToString(), FirstName = "Michael", LastName="Doe", Email = "michael@example.com", About = "Number one hair stylist in Veliko Varnovo", Phone = "0888 555555", IsPhoneVisible = true, CountryId = countryBGId, CityId = velikoTarnovoId, Occupation = occupations[31]},
            new ProfileEntity {Id = Guid.NewGuid().ToString(), FirstName = "Michael", LastName="Soe", Email = "michaels@example.com", About = "Number one hair stylist in Veliko Varnovo", Phone = "0888 555556", IsPhoneVisible = true, CountryId = countryBGId, CityId = velikoTarnovoId, Occupation = occupations[31]},
            new ProfileEntity {Id = Guid.NewGuid().ToString(), FirstName = "Michael", LastName="Boe", Email = "michaelb@example.com", About = "Number one hair stylist in Veliko Varnovo", Phone = "0888 555557", IsPhoneVisible = true, CountryId = countryBGId, CityId = velikoTarnovoId, Occupation = occupations[31]},
            new ProfileEntity {Id = Guid.NewGuid().ToString(), FirstName = "Michael", LastName="Koe", Email = "michaelk@example.com", About = "Number one hair stylist in Veliko Varnovo", Phone = "0888 555558", IsPhoneVisible = true, CountryId = countryBGId, CityId = velikoTarnovoId, Occupation = occupations[31]},
            new ProfileEntity {Id = Guid.NewGuid().ToString(), FirstName = "Michael", LastName="Loe", Email = "michaell@example.com", About = "Number one hair stylist in Veliko Varnovo", Phone = "0888 555559", IsPhoneVisible = true, CountryId = countryBGId, CityId = velikoTarnovoId, Occupation = occupations[31]},
            new ProfileEntity {Id = Guid.NewGuid().ToString(), FirstName = "Michael", LastName="Foe", Email = "michaelf@example.com", About = "Number one hair stylist in Veliko Varnovo", Phone = "0888 555550", IsPhoneVisible = true, CountryId = countryBGId, CityId = velikoTarnovoId, Occupation = occupations[31]}
        };

        onebrbDbContext.Profiles.AddRange(profiles);

        // Add countries & cities
        onebrbDbContext.Countries.AddRange(countries);

        // Add ratings
        var ratings = new RatingEntity[]
        {
            new RatingEntity { Value = 5, AuthorId = profiles[2].Id, RecipientId = profiles[0].Id},
            new RatingEntity { Value = 3, AuthorId = profiles[3].Id, RecipientId = profiles[0].Id},
            new RatingEntity { Value = 5, AuthorId = profiles[4].Id, RecipientId = profiles[0].Id},
            new RatingEntity { Value = 4, AuthorId = profiles[5].Id, RecipientId = profiles[0].Id},
            new RatingEntity { Value = 4, AuthorId = profiles[6].Id, RecipientId = profiles[0].Id},
            new RatingEntity { Value = 5, AuthorId = profiles[7].Id, RecipientId = profiles[0].Id},
            new RatingEntity { Value = 5, AuthorId = profiles[8].Id, RecipientId = profiles[0].Id},
            //new RatingEntity { Value = 5, AuthorId = profiles[8].Id, RecipientId = profiles[0].Id},
        };

        // Add comments
        var comments = new CommentEntity[]
        {
            new CommentEntity {Id = Guid.NewGuid().ToString(), Author =  profiles[2], Content = "This guy is amazing C# developer 1.", DateCreated = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-1)), Recipient = profiles[1]},
            new CommentEntity {Id = Guid.NewGuid().ToString(), Author =  profiles[3], Content = "I've seen better developers!", DateCreated = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-2)), Recipient = profiles[1]},
            new CommentEntity {Id = Guid.NewGuid().ToString(), Author =  profiles[4], Content = "This guy is amazing C# developer 3.", DateCreated = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-2).AddHours(-4)), Recipient = profiles[1]},
            new CommentEntity {Id = Guid.NewGuid().ToString(), Author =  profiles[5], Content = "This guy is amazing C# developer 4.", DateCreated = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-3).AddHours(-5)), Recipient = profiles[1]},
            new CommentEntity {Id = Guid.NewGuid().ToString(), Author =  profiles[6], Content = "This guy is amazing C# developer 5.", DateCreated = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-4).AddHours(-6)), Recipient = profiles[1]},
            new CommentEntity {Id = Guid.NewGuid().ToString(), Author =  profiles[7], Content = "This guy is amazing C# developer 6.", DateCreated = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-5).AddHours(-7)), Recipient = profiles[1]},
            new CommentEntity {Id = Guid.NewGuid().ToString(), Author =  profiles[8], Content = "This guy is amazing C# developer 7.", DateCreated = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-6).AddHours(-8)), Recipient = profiles[1]},
            //new CommentEntity {Id = Guid.NewGuid().ToString(), Author =  profiles[8], Content = "This guy is amazing C# developer 8.", DateCreated = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-7).AddHours(-9)), Recipient = profiles[0]},
        };

        comments[0].Rating = ratings[0];
        comments[1].Rating = ratings[1];
        comments[2].Rating = ratings[2];
        comments[3].Rating = ratings[3];
        comments[4].Rating = ratings[4];
        comments[5].Rating = ratings[5];
        comments[6].Rating = ratings[6];
        //comments[7].Rating = ratings[7];

        onebrbDbContext.Comments.AddRange(comments);

        onebrbDbContext.SaveChanges();
    }
}
