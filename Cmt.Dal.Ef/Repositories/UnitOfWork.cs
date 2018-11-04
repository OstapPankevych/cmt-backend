using System;
using System.Threading.Tasks;
using Cmt.Dal.Interfaces.Repositories;

namespace Cmt.Dal.Ef.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private bool disposed;
        private readonly CmtContext _cmtContext;

        public UnitOfWork(CmtContext cmtContext)
        {
            _cmtContext = cmtContext;
        }

        public Task<int> SaveAsync() => _cmtContext.SaveChangesAsync();

        public void Save() => _cmtContext.SaveChanges();

        public virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                _cmtContext.Dispose();
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
