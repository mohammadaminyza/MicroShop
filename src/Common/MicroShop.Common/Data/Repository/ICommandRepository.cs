using MicroShop.Common.Data.UnitOfWork;
using MicroShop.Common.Entities;
using MicroShop.Common.ValueObjects;
using System.Linq.Expressions;

namespace MicroShop.Common.Data.Repository;

public interface ICommandRepository<TEntity> : IUnitOfWork
    where TEntity : AggregateRoot
{
    void Insert(TEntity entity);
    Task InsertAsync(TEntity entity);

    void InsertRange(IEnumerable<TEntity> entities);
    Task InsertRangeAsync(IEnumerable<TEntity> entities);

    void Delete(Id id);
    void Delete(TEntity entity);

    void DeleteGraph(Id id);

    /// <summary>
    /// یک شی را دریافت کرده و از دیتابیس حذف می‌کند
    /// </summary>
    /// <param name="entities"></param>

    void DeleteRange(IEnumerable<TEntity> entities);

    TEntity GetGraph(Id id);
    Task<TEntity> GetGraphAsync(Id businessId);
    bool Exists(Expression<Func<TEntity, bool>> expression);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
}