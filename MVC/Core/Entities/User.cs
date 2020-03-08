using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MVC.Core.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("surname")]
        public string Surname { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("hashPassword")]
        public string HashPassword { get; set; }

        [BsonElement("friends")]
        public List<Friend> Friends { get; set; }

        [BsonElement("imageSource")]
        public string ImageSource { get; set; }
    }
}
