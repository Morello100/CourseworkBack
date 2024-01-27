    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    namespace mongodb_dotnet_example.Models
    {
        public class SelectedProductsDatabaseSettings : ISelectedProductsDatabaseSettings
        {
            public string CollectionName { get; set; }
            
            [BsonElement("ConnectionString")]
            public string ConnectionString { get; set; }
            
            [BsonElement("DatabaseName")]
            public string DatabaseName { get; set; }
        }

    }
