using BossServer.Models;
using MongoDB.Driver;

namespace BossServer.Data
{
    public interface IMongoDbContext
    {
        IMongoCollection<Customer> Customers { get; }
        IMongoCollection<ApplicationUser> Users { get; }
        IMongoCollection<ChatContext> ChatContexts { get; }
        IMongoCollection<ChatMessage> ChatMessages { get; }
    }
}
