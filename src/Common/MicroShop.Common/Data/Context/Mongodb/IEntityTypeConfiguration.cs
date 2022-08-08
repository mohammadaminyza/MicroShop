namespace MicroShop.Common.Data.Context.Mongodb;

public interface IEntityTypeConfiguration<TEntity>
{
    void Configure(EntityTypeBuilder<TEntity> builder);
}