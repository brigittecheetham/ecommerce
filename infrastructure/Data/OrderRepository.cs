using System.Collections.Generic;
using System.Threading.Tasks;
using core.Entities.OrderAggregate;
using core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using core.RepositoryObjects;

namespace infrastructure.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreContext _context;
        public OrderRepository(StoreContext context)
        {
            _context = context;

        }

        public void Add(Order entity)
        {
            _context.Set<Order>().Add(entity);
        }

        public async Task<int> CountAsync(IRepositoryParameters repositoryParameters)
        {
            var repositoryObject = (OrderRepositoryObject)repositoryParameters;
            var query = GetOrderQuery(repositoryObject);
            return await query.CountAsync();
        }

        public void Delete(Order entity)
        {
            _context.Remove<Order>(entity);
        }

        public async Task<IReadOnlyList<Order>> GetAllAsync(IRepositoryParameters repositoryParameters)
        {
            var repositoryObject = (OrderRepositoryObject)repositoryParameters;
            var query = GetOrderQuery(repositoryObject);
            return await query.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var query = GetOrderQuery(null);
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Order entity)
        {
            _context.Update<Order>(entity);
        }

        private IQueryable<Order> GetOrderQuery(OrderRepositoryObject orderRepositoryObject)
        {
            var query = _context.Orders
                .Include(i => i.OrderItems) 
                .Include(d => d.DeliveryMethod)
                .AsQueryable();

            if (!string.IsNullOrEmpty(orderRepositoryObject?.BuyerEmail))
            {
                query = query.Where(e => e.BuyerEmail == orderRepositoryObject.BuyerEmail);
            }

            if (!string.IsNullOrEmpty(orderRepositoryObject?.PaymentIntentId))
            {
                query = query.Where(e => e.PaymentIntentId == orderRepositoryObject.PaymentIntentId);
            }

            return query;
        }
    }
}