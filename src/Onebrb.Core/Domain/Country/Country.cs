namespace Onebrb.Core.Domain.Country
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
