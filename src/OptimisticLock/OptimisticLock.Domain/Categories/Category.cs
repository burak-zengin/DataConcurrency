namespace OptimisticLock.Domain.Categories;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; }

    public byte[] RowVersion { get; set; }
}
