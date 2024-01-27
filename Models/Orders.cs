using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace mongodb_dotnet_example.Models
{
    public class Orders
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Products")]
        public List<OrderProduct> Products { get; set; }

        [BsonElement("Address")]
        public string Address { get; set; }

        [BsonElement("TotalPrice")]
        public string TotalPrice { get; set; }

        [BsonElement("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [BsonElement("CourierId")]
        public string CourierId { get; set; }
    }

    public class OrderProduct
    {
        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Price")]
        public string Price { get; set; }

        [BsonElement("Total")]
        public string Total { get; set; }
    }
}
