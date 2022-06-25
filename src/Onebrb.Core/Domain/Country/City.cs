namespace Onebrb.Core.Domain.Country
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public Country Country { get; set; }
    }
}
