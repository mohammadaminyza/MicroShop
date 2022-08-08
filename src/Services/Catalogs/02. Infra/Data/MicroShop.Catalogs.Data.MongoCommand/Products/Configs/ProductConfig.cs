using MicroShop.Catalogs.Core.Domain.Common.ValueObjects;
using MicroShop.Catalogs.Core.Domain.Products.Entities;
using MicroShop.Common.Data.Context.Mongodb;

namespace MicroShop.Catalogs.Data.MongoCommand.Products.Configs;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name)
            .HasConversion(n => n.Value, n => Name.FromString(n))
            .IsRequired();
    }
}