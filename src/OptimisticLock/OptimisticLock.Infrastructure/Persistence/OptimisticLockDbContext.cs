using Microsoft.EntityFrameworkCore;
using OptimisticLock.Domain.Categories;
using OptimisticLock.Domain.Products;
using OptimisticLock.Infrastructure.Persistence.Configurations;

namespace OptimisticLock.Infrastructure.Persistence;

public class OptimisticLockDbContext : DbContext
{
    public OptimisticLockDbContext(DbContextOptions<OptimisticLockDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new ProductConfiguration().Configure(modelBuilder.Entity<Product>());
        new CategoryConfiguration().Configure(modelBuilder.Entity<Category>());

        base.OnModelCreating(modelBuilder);
    }
}
