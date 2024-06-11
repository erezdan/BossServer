using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BossServer.Models
{
    public class ApplicationUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public Guid UniqueUserId { get; set; } = Guid.NewGuid();
        public string CustomerId { get; set; } = string.Empty;

        // Login details
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // Personal details
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Roles
        public List<string> Roles { get; set; } = new List<string>();
    }

}