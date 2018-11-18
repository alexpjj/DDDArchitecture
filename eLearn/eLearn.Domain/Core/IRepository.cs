using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Domain.Core
{
    public interface IRepository : IDisposable
    {
        IUnitOfWork IUnitOfWork { get; }
    }

    public interface IRepository<TEntity, TId> : IRepository where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync(bool isTrackeable, params Expression<Func<TEntity, object>>[] includeReferences);

        Task<List<TEntity>> GetAllByExpressionAsync(bool isTrackeable, Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeReferences);

        Task<TEntity> GetByIdAsync(TId id);

        Task<TEntity> GetByIdAsync(TId Id, bool isTrackeable, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetByExpressionAsync(bool isTrackeable, Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeReferences);

        Task<TResult> GetSelectByExpressionAsync<TResult>(bool isTrackeable, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TResult>> select, params Expression<Func<TEntity, object>>[] includeReferences);

        Task<PageResult<TEntity>> SearchPagedAsync(Func<TEntity, object> orderBy, int page, int pageSize, bool orderByDescencing = false, params Expression<Func<TEntity, object>>[] includeReferences);

        Task<PageResult<TEntity>> SearchPagedAsync(Func<TEntity, object> orderBy, int page, int pageSize, Expression<Func<TEntity, bool>> where, bool orderByDescencing = false, params Expression<Func<TEntity, object>>[] includeReferences);

        Task<List<TResult>> SearchForAsync<TResult>(string orderBy, int page, int pageSize, Expression<Func<TEntity, bool>> callback, Expression<Func<TEntity, TResult>> select, params Expression<Func<TEntity, object>>[] includeReferences);

        Func<TEntity, object> MakeOrderBy(Func<TEntity, object> orderBy);

        void Create(TEntity entity);

        Task UpdateAsync(TId id, TEntity newEntity);

        void Update(TEntity obj, params Expression<Func<TEntity, object>>[] propertiesToUpdate);

        void Delete(TEntity entity);

        Task DeleteAsync(TId Id);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> where);
    }
}
