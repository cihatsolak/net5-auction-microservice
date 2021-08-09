using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ESourcing.Products.Entities
{
    public class Product
    {
        [BsonId] //primary key
        [BsonRepresentation(BsonType.ObjectId)] //identity 1-1
        public string Id { get; set; }

        [BsonElement("Name")] //[DisplayName("Name")]
        public string Name { get; set; }
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
    }
}
