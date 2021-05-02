using System;
using System.Threading.Tasks;
using core.Entities;

namespace core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Complete();
        IRepository<T> Repository<T>() where T : BaseEntity;
    }
}