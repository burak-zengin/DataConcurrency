using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OptimisticLock.Domain.Products;

namespace OptimisticLock.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property<Guid>(_ => _.ConcurrencyToken).IsConcurrencyToken();
    }
}
