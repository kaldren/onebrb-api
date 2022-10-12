using Onebrb.Domain.Entities.CountryAggregate;
using Onebrb.Domain.Entities.MessageAggregate;
using Onebrb.Domain.Entities.ProductAggregate;
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

        // Add product
        string productId = "16e9bd28-2ac3-4d92-9186-d91cfc95cb52";
        var product = new Product()
        {
            Id = productId,
            Date = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-1)),
            Title = "Tesla Model S 2022",
            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution.",
            Price = 141000M,
            Images = new List<ProductImage>()
            {
                new ProductImage() { Id = Guid.NewGuid().ToString(), Path = "https://automedia.investor.bg/media/files/resized/uploadedfiles/640x0/306/7ea7096ab1efa8cd5d823a7ea8a4b306-tesla-model-s-2022-update-2.jpg" },
                new ProductImage() { Id = Guid.NewGuid().ToString(), Path = "https://automedia.investor.bg/media/files/resized/uploadedfiles/640x0/306/7ea7096ab1efa8cd5d823a7ea8a4b306-tesla-model-s-2022-update-2.jpg" },
                new ProductImage() { Id = Guid.NewGuid().ToString(), Path = "https://automedia.investor.bg/media/files/resized/uploadedfiles/640x0/306/7ea7096ab1efa8cd5d823a7ea8a4b306-tesla-model-s-2022-update-2.jpg" },
                new ProductImage() { Id = Guid.NewGuid().ToString(), Path = "https://automedia.investor.bg/media/files/resized/uploadedfiles/640x0/306/7ea7096ab1efa8cd5d823a7ea8a4b306-tesla-model-s-2022-update-2.jpg" },
                new ProductImage() { Id = Guid.NewGuid().ToString(), Path = "https://automedia.investor.bg/media/files/resized/uploadedfiles/640x0/306/7ea7096ab1efa8cd5d823a7ea8a4b306-tesla-model-s-2022-update-2.jpg" },
                new ProductImage() { Id = Guid.NewGuid().ToString(), Path = "https://automedia.investor.bg/media/files/resized/uploadedfiles/640x0/306/7ea7096ab1efa8cd5d823a7ea8a4b306-tesla-model-s-2022-update-2.jpg" },
            }
        };

        string product2Id = "16e9bd28-2ac3-4d92-9186-d91cfc95cb53";
        var product2 = new Product()
        {
            Id = product2Id,
            Date = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-1)),
            Title = "Tesla Model S 2023",
            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution.",
            Price = 141000M,
            Images = new List<ProductImage>()
            {
                new ProductImage() { Id = Guid.NewGuid().ToString(), Path = "https://automedia.investor.bg/media/files/resized/uploadedfiles/640x0/306/7ea7096ab1efa8cd5d823a7ea8a4b306-tesla-model-s-2022-update-2.jpg" },
                new ProductImage() { Id = Guid.NewGuid().ToString(), Path = "https://automedia.investor.bg/media/files/resized/uploadedfiles/640x0/306/7ea7096ab1efa8cd5d823a7ea8a4b306-tesla-model-s-2022-update-2.jpg" },
                new ProductImage() { Id = Guid.NewGuid().ToString(), Path = "https://automedia.investor.bg/media/files/resized/uploadedfiles/640x0/306/7ea7096ab1efa8cd5d823a7ea8a4b306-tesla-model-s-2022-update-2.jpg" },
                new ProductImage() { Id = Guid.NewGuid().ToString(), Path = "https://automedia.investor.bg/media/files/resized/uploadedfiles/640x0/306/7ea7096ab1efa8cd5d823a7ea8a4b306-tesla-model-s-2022-update-2.jpg" },
                new ProductImage() { Id = Guid.NewGuid().ToString(), Path = "https://automedia.investor.bg/media/files/resized/uploadedfiles/640x0/306/7ea7096ab1efa8cd5d823a7ea8a4b306-tesla-model-s-2022-update-2.jpg" },
                new ProductImage() { Id = Guid.NewGuid().ToString(), Path = "https://automedia.investor.bg/media/files/resized/uploadedfiles/640x0/306/7ea7096ab1efa8cd5d823a7ea8a4b306-tesla-model-s-2022-update-2.jpg" },
            }
        };


        // Add profiles
        var profiles = new Profile[]
        {
            new Profile {Id = profile1Id, FirstName = "Kaloyan", LastName="Drenski", Email = "drenski666@gmail.com", About = "Number one software developer in Veliko Varnovo", Phone = "0888 778877", CountryId = countryBGId, CityId = velikoTarnovoId},
            new Profile {Id = profile2Id, FirstName = "Bob", LastName="Doe", Email = "bob@example.com", About = "Number one real estate broker in Veliko Varnovo", Phone = "0888 222222", CountryId = countryBGId, CityId = velikoTarnovoId},
            new Profile {Id = Guid.NewGuid().ToString(), FirstName = "Steve", LastName="Doe", Email = "steve@example.com", About = "Number one taxi driver in Veliko Varnovo", Phone = "0888 333333", CountryId = countryBGId, CityId = velikoTarnovoId},
            new Profile {Id = Guid.NewGuid().ToString(), FirstName = "Garry", LastName="Doe", Email = "garry@example.com", About = "Number one zumba trainer in Veliko Varnovo", Phone = "0888 444444", CountryId = countryBGId, CityId = velikoTarnovoId},
            new Profile {Id = Guid.NewGuid().ToString(), FirstName = "Michael", LastName="Doe", Email = "michael@example.com", About = "Number one hair stylist in Veliko Varnovo", Phone = "0888 555555", CountryId = countryBGId, CityId = velikoTarnovoId}
        };

        // Add product to user Kaloyan Drenski
        profiles[0].Products = new List<Product>()
        {
            product,
            product2
        };

        onebrbDbContext.Products.AddRange(product);
        onebrbDbContext.Profiles.AddRange(profiles);

        // Add countries & cities
        onebrbDbContext.Countries.AddRange(countries);

        // Add comments
        var comments = new Comment[]
        {
            new Comment {Id = Guid.NewGuid().ToString(), Author =  profiles[1], Content = "This guy is amazing C# developer.", Date = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-1)), Recipient = profiles[0]},
            new Comment {Id = Guid.NewGuid().ToString(), Author =  profiles[2], Content = "This guy is amazing C# developer.", Date = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-2)), Recipient = profiles[0]},
            new Comment {Id = Guid.NewGuid().ToString(), Author =  profiles[3], Content = "This guy is amazing C# developer.", Date = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(-2).AddHours(-4)), Recipient = profiles[0]},
        };
        onebrbDbContext.Comments.AddRange(comments);

        var messages = new Message[]
        {
            new Message
            {
                AuthorFirstName = profiles[1].FirstName,
                AuthorLastName = profiles[1].LastName,
                Content = "Fake message content",
                AuthorId = profiles[1].Id,
                Id = Guid.NewGuid().ToString(),
                RecipientFirstName = profiles[0].FirstName,
                RecipientLastName = profiles[0].LastName,
                RecipientId = profiles[0].Id,
                Title = "Hey fake title!",
                Date = DateTime.UtcNow.AddHours(-2),
            }
        };

        var conversations = new Conversation()
        {
            Messages = messages,
        };

        onebrbDbContext.Conversations.AddRange(conversations);

        onebrbDbContext.SaveChanges();
    }
}
