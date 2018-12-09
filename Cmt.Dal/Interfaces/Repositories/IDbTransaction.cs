using System;

namespace Cmt.Dal.Interfaces.Repositories
{
    public interface IDbTransaction: IDisposable
    {
        void Commit();
        void Rollback();
    }
}
