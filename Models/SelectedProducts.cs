    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System.Collections.Generic;

    namespace mongodb_dotnet_example.Models
    {
    public class SelectedProducts
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("UserId")]
        public string UserId { get; set; }

        [BsonElement("SelectedProductsList")]
        public List<SelectedProduct> SelectedProductsList { get; set; }
    }

    }

        public class SelectedProduct
        {
            [BsonElement("Title")]
            public string Title { get; set; }

            [BsonElement("Price")]
            public string Price { get; set; }

            [BsonElement("Count")]
            public string Count { get; set; }
        }

