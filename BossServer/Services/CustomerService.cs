using BossServer.Data;
using BossServer.Models;
using MongoDB.Driver;

namespace BossServer.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomerService(IMongoDbContext context)
        {
            _customers = context.Customers;
        }

        public async Task<List<Customer>> GetAsync() =>
            await _customers.Find(_ => true).ToListAsync();

        public async Task<Customer> GetAsync(string id) =>
            await _customers.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Customer newCustomer) =>
            await _customers.InsertOneAsync(newCustomer);

        public async Task UpdateAsync(string id, Customer updatedCustomer) =>
            await _customers.ReplaceOneAsync(x => x.Id == id, updatedCustomer);

        public async Task RemoveAsync(string id) =>
            await _customers.DeleteOneAsync(x => x.Id == id);
    }
}
