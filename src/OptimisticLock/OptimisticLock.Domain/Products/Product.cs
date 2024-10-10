namespace OptimisticLock.Domain.Products;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public Guid ConcurrencyToken { get; set; }
}
