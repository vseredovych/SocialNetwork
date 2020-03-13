using MongoDB.Bson.Serialization.Attributes;

namespace MVC.Core.Entities
{
    public class Like
    {
        [BsonElement("email")]
        public string Email { get; set; }
    }
}
