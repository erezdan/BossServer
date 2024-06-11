﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BossServer.Models
{
    public class ChatContext
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public List<string> MessageIds { get; set; } = new List<string>(); // List of message IDs in the chat
    }

}
