using System.Linq.Expressions;
using MicroserviceCourse.Order.Domain.Entities;

namespace MicroserviceCourse.Order.Application.Contracts.Repositories;

public interface IGenericRepository<TId, TEntity> where TId : struct where TEntity : BaseEntity<TId>
{
    public Task<bool> AnyAsync(TId id);

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    Task<List<TEntity>> GetAllAsync();

    Task<List<TEntity>> GetAllPagedAsync(int pageNumber, int pageSize);

    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

    ValueTask<TEntity?> GetByIdAsync(TId id);

    void AddAsync(TEntity entity);

    void Update(TEntity entity);

    void Remove(TEntity entity);
}