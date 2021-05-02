using System.Collections.Generic;
using System.Threading.Tasks;
using core.Entities.OrderAggregate;
using core.Interfaces;
using core.RepositoryObjects;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data
{
    public class DeliveryMethodRepository : IDeliveryMethodRepository
    {
        private readonly StoreContext _context;

        public DeliveryMethodRepository(StoreContext context)
        {
            _context = context;
        }

        public void Add(DeliveryMethod entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> CountAsync(IRepositoryParameters repositoryParameters)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(DeliveryMethod entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetAllAsync(IRepositoryParameters repositoryParameters)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DeliveryMethod> GetByIdAsync(int id)
        {
            return await _context.DeliveryMethods.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(DeliveryMethod entity)
        {
            throw new System.NotImplementedException();
        }
    }
}