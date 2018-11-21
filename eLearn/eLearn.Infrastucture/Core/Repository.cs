using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LinqKit;
using System.Diagnostics.CodeAnalysis;
using eLearn.Domain.Core;
using eLearn.Infrastucture.Context;
using eLearn.Infrastucture.Extensions;

namespace eLearn.Infrastucture.Core
{
    [ExcludeFromCodeCoverage]
    public abstract class Repository
    {
    }

    [ExcludeFromCodeCoverage]
    public abstract class Repository<TEntity, TId> : Repository, IRepository<TEntity, TId> where TEntity : class
    {
        private readonly IQueryableUnitOfWork unitOfWork;

        public Repository(IQueryableUnitOfWork IUnitOfWork)
        {
            if (IUnitOfWork == null)
                throw new ArgumentNullException(nameof(IUnitOfWork));

            this.unitOfWork = IUnitOfWork;
        }

        public IUnitOfWork IUnitOfWork
        {
            get
            {
                return this.unitOfWork;
            }
        }

        public void Create(TEntity entity)
        {
            this.GetSet().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            //set as "removed"
            this.GetSet().Remove(entity);
        }

        public async Task DeleteAsync(TId Id)
        {
            var ent = await this.GetByIdAsync(Id).ConfigureAwait(false);

            this.GetSet().Remove(ent);
        }

        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }

        public Task<List<TEntity>> GetAllAsync(bool isTrackeable, params Expression<Func<TEntity, object>>[] includeReferences)
        {
            IQueryable<TEntity> query = this.GetSet().AsQueryable();

            if (!isTrackeable)
                query = query.AsNoTracking();

            if (includeReferences != null && includeReferences.Length > 0)
                query = includeReferences.Aggregate(query, System.Data.Entity.QueryableExtensions.Include);

            return query.ToListAsync();
        }

        public Task<List<TEntity>> GetAllByExpressionAsync(bool isTrackeable, Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeReferences)
        {
            IQueryable<TEntity> query = this.GetSet().AsQueryable();

            if (!isTrackeable)
                query = query.AsNoTracking();

            if (includeReferences != null && includeReferences.Length > 0)
                query = includeReferences.Aggregate(query, System.Data.Entity.QueryableExtensions.Include);


            return query.AsExpandable().Where(expression).ToListAsync();
        }

        public Task<List<TResult>> GetAllByExpressionAsync<TResult>(bool isTrackeable, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TResult>> select, params Expression<Func<TEntity, object>>[] includeReferences)
        {
            IQueryable<TEntity> query = this.GetSet().AsQueryable();

            if (!isTrackeable)
                query = query.AsNoTracking();

            if (includeReferences != null && includeReferences.Length > 0)
                query = includeReferences.Aggregate(query, System.Data.Entity.QueryableExtensions.Include);

            return query
                .AsExpandable()
                .Where(expression)
                .Select(select)
                .ToListAsync();
        }
        
        public Task<List<TEntity>> GetAllByFuncAsync(bool isTrackeable, Func<TEntity, bool> expression, params Expression<Func<TEntity, object>>[] includeReferences)
        {
            IQueryable<TEntity> query = this.GetSet().AsQueryable();

            if (!isTrackeable)
                query = query.AsNoTracking();

            if (includeReferences != null && includeReferences.Length > 0)
                query = includeReferences.Aggregate(query, System.Data.Entity.QueryableExtensions.Include);

            return query.AsExpandable().Where(expression).AsQueryable().ToListAsync();
        }
        
        public Task<TEntity> GetByIdAsync(TId id)
        {
            return (this.GetSet() as DbSet<TEntity>).FindAsync(id);
        }

        public Task<TEntity> GetByIdAsync(TId Id, bool isTrackeable, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var propertyName = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)this.IUnitOfWork).ObjectContext
                    .CreateObjectSet<TEntity>().EntitySet.ElementType.KeyMembers.Single().Name;

            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var predicate = Expression.Lambda<Func<TEntity, bool>>(
                Expression.Equal(
                    Expression.PropertyOrField(parameter, propertyName),
                    Expression.Constant(Id, typeof(TId))),
                parameter);

            IQueryable<TEntity> query = this.GetSet().AsQueryable();

            if (!isTrackeable)
                query = query.AsNoTracking();

            if (includeProperties != null && includeProperties.Length > 0)
                query = includeProperties.Aggregate(query, System.Data.Entity.QueryableExtensions.Include);

            return query.FirstOrDefaultAsync(predicate);
        }

        public Task<TEntity> GetByExpressionAsync(bool isTrackeable, Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeReferences)
        {
            IQueryable<TEntity> query = this.GetSet().AsQueryable();

            if (!isTrackeable)
                query = query.AsNoTracking();

            if (includeReferences != null && includeReferences.Length > 0)
                query = includeReferences.Aggregate(query, System.Data.Entity.QueryableExtensions.Include);

            return query.AsExpandable().FirstOrDefaultAsync(expression);
        }

        public Task<TResult> GetSelectByExpressionAsync<TResult>(bool isTrackeable, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TResult>> select, params Expression<Func<TEntity, object>>[] includeReferences)
        {
            IQueryable<TEntity> query = this.GetSet().AsQueryable();

            if (!isTrackeable)
                query = query.AsNoTracking();

            if (includeReferences != null && includeReferences.Length > 0)
                query = includeReferences.Aggregate(query, System.Data.Entity.QueryableExtensions.Include);

            return query
                .AsExpandable()
                .Where(expression)
                .Select(select)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(TId id, TEntity newEntity)
        {
            var olderEntity = await this.GetByIdAsync(id).ConfigureAwait(false);

            if (olderEntity == null)
            {
                throw new NullReferenceException("don't find the entity with the id " + id.ToString());
            }

            this.unitOfWork.Update<TEntity>(olderEntity, newEntity);
        }

        public void Update(TEntity obj, params Expression<Func<TEntity, object>>[] propertiesToUpdate)
        {
            this.unitOfWork.Update<TEntity>(obj, propertiesToUpdate);
        }

        protected IDbSet<TEntity> GetSet()
        {
            return this.unitOfWork.CreateSet<TEntity>();
        }

        public Task<PageResult<TEntity>> SearchPagedAsync(Func<TEntity, object> orderBy, int page, int pageSize, bool orderByDescencing = false, params Expression<Func<TEntity, object>>[] includeReferences)
        {
            return this.SearchPagedAsync(orderBy, page, pageSize, x => true, orderByDescencing, includeReferences);
        }

        public async Task<PageResult<TEntity>> SearchPagedAsync(Func<TEntity, object> orderBy, int page, int pageSize, Expression<Func<TEntity, bool>> where, bool orderByDescencing = false, params Expression<Func<TEntity, object>>[] includeReferences)
        {
            int total = 0;

            total = await this.GetSet().AsQueryable().AsNoTracking().AsExpandable().Where(where).CountAsync().ConfigureAwait(false);

            IEnumerable<TEntity> dataResult;

            if (total > 0)
            {
                IQueryable<TEntity> query = this.GetSet().AsQueryable().AsNoTracking().AsExpandable();

                if (includeReferences != null && includeReferences.Length > 0)
                    query = includeReferences.Aggregate(query, System.Data.Entity.QueryableExtensions.Include);

                IOrderedEnumerable<TEntity> queryOrder = null;

                if (where != null)
                {
                    query = query.Where(where);
                }

                if (orderByDescencing)
                {
                    queryOrder = query.OrderByDescending(orderBy);
                }
                else
                {
                    queryOrder = query.OrderBy(orderBy);
                }

                dataResult = await queryOrder
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .AsQueryable()
                    .ToListAsync().ConfigureAwait(false);
            }
            else
            {
                dataResult = new TEntity[] { };
            }

            return new PageResult<TEntity>() { Items = dataResult, Total = total };
        }
        public Func<TEntity, object> MakeOrderBy(Func<TEntity, object> orderBy)
        {
            return orderBy;
        }

        public Task<List<TResult>> SearchForAsync<TResult>(string orderBy, int page, int pageSize, Expression<Func<TEntity, bool>> callback, Expression<Func<TEntity, TResult>> select, params Expression<Func<TEntity, object>>[] includeReferences)
        {
            IQueryable<TEntity> query = this.GetSet().AsQueryable().AsNoTracking();

            if (includeReferences != null && includeReferences.Length > 0)
                query = includeReferences.Aggregate(query, System.Data.Entity.QueryableExtensions.Include);

            return query
                .AsExpandable()
                .Where(callback)
                .Select(select)
                .OrderBy(orderBy)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> where)
        {
            IQueryable<TEntity> query = this.GetSet().AsQueryable().AsNoTracking();

            return query.CountAsync(where);
        }

        protected IQueryable<TEntity> GetQueryable()
        {
            return this.GetSet().AsQueryable();
        }

        protected IQueryable<TEntity> GetQueryableAsNoTracking()
        {
            return this.GetSet().AsQueryable().AsNoTracking();
        }
    }
}
