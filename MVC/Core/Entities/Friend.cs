using MongoDB.Bson.Serialization.Attributes;

namespace MVC.Core.Entities
{
    public class Friend
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("surname")]
        public string Surname { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("imageSource")]
        public string ImageSource { get; set; }
    }
}