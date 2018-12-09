using System;
using Cmt.Dal.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cmt.Dal.Ef.Repositories
{
    public class DbTransaction: IDbTransaction, IDisposable
    {
        private bool disposed;
        private readonly IDbContextTransaction _transaction;

        public DbTransaction(IDbContextTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                _transaction.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
