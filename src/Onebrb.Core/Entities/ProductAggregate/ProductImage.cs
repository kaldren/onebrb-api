namespace Onebrb.Domain.Entities.ProductAggregate;

public class ProductImage : BaseEntity
{
    public string Path { get; set; }

    public string ProductId { get; set; }

    public virtual Product Product { get; set; }
}
