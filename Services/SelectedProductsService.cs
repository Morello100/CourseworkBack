using mongodb_dotnet_example.Models;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace mongodb_dotnet_example.Services
{
    public class SelectedProductsService
    {
        private readonly IMongoCollection<SelectedProducts> _selectedProducts;

        public SelectedProductsService(string connectionString, string databaseName)
        {
            Console.WriteLine($"ConnectionString in SelectedProductsService: {connectionString}");
            Console.WriteLine($"DatabaseName in SelectedProductsService: {databaseName}");

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _selectedProducts = database.GetCollection<SelectedProducts>("selectedProducts");
        }

        public List<SelectedProducts> Get() => _selectedProducts.Find(selectedProducts => true).ToList();

        public SelectedProducts Get(string id) => _selectedProducts.Find(selectedProducts => selectedProducts.UserId == id).FirstOrDefault();

        public SelectedProducts Create(string userId)
        {
            var selectedProducts = new SelectedProducts
            {
                UserId = userId,
                SelectedProductsList = new List<SelectedProduct>()
            };

            _selectedProducts.InsertOne(selectedProducts);

            return selectedProducts;
        }

        public void Update(string id, SelectedProducts updatedSelectedProducts) => _selectedProducts.ReplaceOne(selectedProducts => selectedProducts.Id == id, updatedSelectedProducts);

public void UpdateSelectedProductField(string id, List<SelectedProduct> updatedValue)
{
    var filter = Builders<SelectedProducts>.Filter.Eq(sp => sp.Id, id);
    var update = Builders<SelectedProducts>.Update.Set(sp => sp.SelectedProductsList, updatedValue);

    _selectedProducts.UpdateOne(filter, update);
}



        public void Delete(SelectedProducts selectedProductsForDeletion) => _selectedProducts.DeleteOne(selectedProducts => selectedProducts.Id == selectedProductsForDeletion.Id);

        public void Delete(string id) => _selectedProducts.DeleteOne(selectedProducts => selectedProducts.Id == id);
    }
}
