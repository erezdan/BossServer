using BossServer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BossServer.Data
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _db;

        public MongoDbContext(IOptions<MongoDBSettings> options)
        {
            var settings = options.Value;
            var client = new MongoClient(settings.ConnectionString);
            _db = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<Customer> Customers => _db.GetCollection<Customer>("Customers");
        public IMongoCollection<ApplicationUser> Users => _db.GetCollection<ApplicationUser>("Users");
        public IMongoCollection<ChatContext> ChatContexts => _db.GetCollection<ChatContext>("ChatContexts");
        public IMongoCollection<ChatMessage> ChatMessages => _db.GetCollection<ChatMessage>("ChatMessages");
    }

}
