using System;

namespace Cmt.Dal.Interfaces.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        IDbTransaction BeginTransaction();
    }
}
