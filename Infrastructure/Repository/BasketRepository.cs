using Domian.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class BasketRepository<T> : IBasketRepository<T> where T : class
    {
        private readonly IDatabase _database;

        

        public BasketRepository(IConnectionMultiplexer redis)
        {
                _database = redis.GetDatabase();
        }
        public async Task<T> DeleteBasketAsync(string BasketId)
        {
            if (BasketId == null)
                return null;
            await _database.KeyDeleteAsync(BasketId);
            return null;
            

        }

        public async Task<T> GetBasketAcync(string BasketId)
        {
             if(!string.IsNullOrEmpty(BasketId))
            {
               var data = await _database.StringGetAsync(BasketId);
                return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<T>(data);
            }
            return default;
        }

        public async Task<T> UpdateBasketAsync(CustomerBasket basket)
        {
            if (basket.Id == null)
            {
                throw new ArgumentNullException("key");
            }
            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if (!created)
                return null;
            return await GetBasketAcync(basket.Id);
        }
    }
}
