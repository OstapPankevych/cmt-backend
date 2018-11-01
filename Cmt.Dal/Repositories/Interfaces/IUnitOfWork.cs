using System;
using System.Threading.Tasks;

namespace Cmt.Dal.Repositories.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        Task<int> SaveAsync();
        void Save();
    }
}
