using Onebrb.Core.Domain.Profile;

namespace Onebrb.Infrastructure
{
    public static class DbInitializer
    {
        public static void Initialize(OnebrbDbContext onebrbDbContext)
        {
            if (onebrbDbContext.Profiles.Any())
            {
                return;
            }

            var profiles = new Profile[]
            {
                new Profile {FirstName = "John", LastName="Doe", Email = "john@example.com", About = "Number one plumber in Veliko Varnovo", Phone = "0888 123456"},
                new Profile {FirstName = "Bob", LastName="Doe", Email = "bob@example.com", About = "Number one real estate broker in Veliko Varnovo", Phone = "0888 222222"},
                new Profile {FirstName = "Steve", LastName="Doe", Email = "steve@example.com", About = "Number one taxi driver in Veliko Varnovo", Phone = "0888 333333"},
                new Profile {FirstName = "Garry", LastName="Doe", Email = "garry@example.com", About = "Number one zumba trainer in Veliko Varnovo", Phone = "0888 444444"},
                new Profile {FirstName = "Michael", LastName="Doe", Email = "michael@example.com", About = "Number one hair stylist in Veliko Varnovo", Phone = "0888 555555"}
            };

            onebrbDbContext.Profiles.AddRange(profiles);
            onebrbDbContext.SaveChanges();
        }
    }
}
