using eLearn.Domain.Core;
using eLearn.Infrastucture.CrossCutting.Exceptions;
using eLearn.Infrastucture.Migration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Infrastucture.Context
{
    [ExcludeFromCodeCoverage]
    public class ContextUnitOfWork : DbContext, IQueryableUnitOfWork
    {
        private ContextUnitOfWorkTransaction currentTransaction;

        public ContextUnitOfWork() : base("eLearn")
        {
        }

        public ContextUnitOfWork(string name) : base(name)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ContextUnitOfWork, Configuration>(name));

            this.ConfigureDebug();
        }

        [System.Diagnostics.Conditional("DEBUG")]
        private void ConfigureDebug()
        {
            this.Database.Log = (l) => System.Diagnostics.Debug.WriteLine(l);
        }

        public IDbSet<T> CreateSet<T>() where T : class
        {
            return this.Set<T>();
        }

        public void RollBack()
        {
            this.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = System.Data.Entity.EntityState.Unchanged);
        }

        public void Update<T>(T olderEntity, T newEntity) where T : class
        {
            this.Entry<T>(olderEntity).CurrentValues.SetValues(newEntity);
        }

        public void Update<T>(T obj, params Expression<Func<T, object>>[] propertiesToUpdate) where T : class
        {
            this.Set<T>().Attach(obj);
            propertiesToUpdate.ToList().ForEach(p => Entry(obj).Property(p).IsModified = true);
        }

        public void SetUnchanged<T>(T entity) where T : class
        {
            this.Entry<T>(entity).State = EntityState.Unchanged;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>()
                .Configure(x => x.HasMaxLength(150));

            base.OnModelCreating(modelBuilder);
        }

        public async Task CommitAsync()
        {
            try
            {
                await this.SaveChangesAsync();
            }
            catch (DbUpdateException updateException)
            {
                var sqlException = updateException?.InnerException?.InnerException as System.Data.SqlClient.SqlException;

                if (sqlException != null)
                {
                    if (sqlException.HResult == -2146232060)
                    {
                        throw new EntityDuplicationException(sqlException.Message);
                    }
                }

                throw updateException;
            }
        }

        public IUnitOfWorkTransaction BeginTransaction()
        {
            if (this.currentTransaction == null)
            {
                this.currentTransaction = new ContextUnitOfWorkTransaction(this);
            }

            return this.currentTransaction;
        }
    }
}
