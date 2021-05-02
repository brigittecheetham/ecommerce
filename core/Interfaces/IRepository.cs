using System.Collections.Generic;
using System.Threading.Tasks;
using core.Entities;
using core.RepositoryObjects;

namespace core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
         Task<T> GetByIdAsync(int id);
         Task<IReadOnlyList<T>> GetAllAsync(IRepositoryParameters repositoryParameters);
         Task<int> CountAsync(IRepositoryParameters repositoryParameters);
         void Add(T entity);
         void Update(T entity);
         void Delete(T entity);
    }
}