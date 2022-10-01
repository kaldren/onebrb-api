namespace Onebrb.Domain.Entities.CountryAggregate;

public class Country : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<City>? Cities { get; set; }
}
