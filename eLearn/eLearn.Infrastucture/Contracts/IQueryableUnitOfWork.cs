using eLearn.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace eLearn.Infrastucture.Context
{
    public interface IQueryableUnitOfWork : IUnitOfWork
    {
        IDbSet<T> CreateSet<T>() where T : class;

        void Update<T>(T olderEntity, T newEntity) where T : class;

        void Update<T>(T obj, params Expression<Func<T, object>>[] propertiesToUpdate) where T : class;

        void SetUnchanged<T>(T entity) where T : class;

    }    
}
