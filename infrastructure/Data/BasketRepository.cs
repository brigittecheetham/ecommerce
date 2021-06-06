using System;
using System.Text.Json;
using System.Threading.Tasks;
using core.Entities;
using core.Interfaces;
using StackExchange.Redis;

namespace infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            Console.WriteLine(basketId);
            Console.WriteLine(_database.Database.ToString() + " " + _database.Multiplexer.ClientName + " " + _database.Multiplexer.Configuration);
            var data = await _database.StringGetAsync(basketId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            var created = await _database.StringSetAsync(customerBasket.Id, JsonSerializer.Serialize<CustomerBasket>(customerBasket), TimeSpan.FromDays(30));

            if (!created)
                return null;

            return await GetBasketAsync(customerBasket.Id);
        }
    }
}