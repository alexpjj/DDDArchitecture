using eLearn.Domain.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Infrastucture.Context
{
    public class ContextUnitOfWorkTransaction : IUnitOfWorkTransaction
    {
        private readonly ContextUnitOfWork contextUnitOfWork;
        private readonly DbContextTransaction dbTransaction;

        public ContextUnitOfWorkTransaction(ContextUnitOfWork contextUnitOfWork)
        {
            this.contextUnitOfWork = contextUnitOfWork;
            this.dbTransaction = this.contextUnitOfWork.Database.BeginTransaction();
        }

        public void Commit()
        {
            this.dbTransaction.Commit();
        }

        public void Dispose()
        {
            this.dbTransaction.Dispose();
        }

        public void RollBack()
        {
            this.dbTransaction.Rollback();
        }
    }
}
