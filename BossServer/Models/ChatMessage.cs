using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BossServer.Models
{
    public class ChatMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string ChatContextId { get; set; } = string.Empty;
    }

}
