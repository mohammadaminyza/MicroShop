namespace MicroShop.Common.Data.Context.Mongodb;

/// <summary>
/// Configuration for database documents
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IEntityTypeConfiguration<TEntity>
{
    void Configure(EntityTypeBuilder<TEntity> builder);
}