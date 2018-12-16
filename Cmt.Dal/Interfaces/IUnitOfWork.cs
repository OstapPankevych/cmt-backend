using System;
using Cmt.Dal.Interfaces.Repositories;

namespace Cmt.Dal.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IDbTransaction BeginTransaction();
    }
}
