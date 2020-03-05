using MongoDB.Bson.Serialization.Attributes;

namespace MVC.Core.Entities
{
    public class Friend
    {
        [BsonElement("email")]
        public string Email { get; set; }
    }
}
