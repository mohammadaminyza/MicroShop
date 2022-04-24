using System.Linq.Expressions;

namespace MicroShop.Common.Data.Context.Mongodb;

public static class CollectionExtension
{
    #region Any

    public static bool Any<TEntity>(this IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>> expression)
    {
        return collection.FindSync(expression).Any();
    }
    public static async Task<bool> AnyAsync<TEntity>(this IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>> expression)
    {
        return await collection.FindSync(expression).AnyAsync();
    }

    #endregion
}