using mongodb_dotnet_example.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mongodb_dotnet_example.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _product;

        public ProductService(string connectionString, string databaseName)
        {
            Console.WriteLine($"ConnectionString in ProductService: {connectionString}");
            Console.WriteLine($"DatabaseName in ProductService: {databaseName}");

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _product = database.GetCollection<Product>("product");
        }

        public List<Product> Get() => _product.Find(product => true).ToList();

        public Product Get(string id)
        {
            var objectId = ObjectId.Parse(id);
            return _product.Find(product => product.Id == objectId.ToString()).FirstOrDefault();
        }

        public Product Create(Product product)
        {
            _product.InsertOne(product);
            return product;
        }

        public void Update(string id, Product updatedProduct)
        {
            var objectId = ObjectId.Parse(id);
            updatedProduct.Id = objectId.ToString();
            _product.ReplaceOne(product => product.Id == objectId.ToString(), updatedProduct);
        }

        public void Delete(string id)
        {
            var objectId = ObjectId.Parse(id);
            _product.DeleteOne(product => product.Id == objectId.ToString());
        }
    }
}
