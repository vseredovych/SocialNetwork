using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MVC.DAL.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("surname")]
        public string Surname { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("hashPassword")]
        public string HashPassword { get; set; }

        [BsonElement("friends")]
        public Friend[] Friends { get; set; }
    }
}
