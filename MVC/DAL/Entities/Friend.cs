using MongoDB.Bson.Serialization.Attributes;

namespace MVC.DAL.Entities
{
    public class Friend
    {
        [BsonElement("email")]
        public string Email { get; set; }
    }
}
