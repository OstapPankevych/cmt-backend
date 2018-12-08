using System;
using System.Threading.Tasks;

namespace Cmt.Dal.Interfaces.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        Task<int> SaveAsync();
        void Save();
    }
}
