using System;
using Cmt.Dal.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cmt.Dal.Ef.Repositories
{
    public class UnitOfWork<T>: IUnitOfWork, IDisposable
        where T: DbContext
    {
        private bool disposed;
        protected readonly T DbContext;

        public UnitOfWork(T dbContext)
        {
            DbContext = dbContext;
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                DbContext.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IDbTransaction BeginTransaction()
        {
            return new DbTransaction(DbContext.Database.BeginTransaction());
        }
    }
}
