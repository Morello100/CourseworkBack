using mongodb_dotnet_example.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Extensions.Options;


namespace mongodb_dotnet_example.Services
{
    public class OrdersService
    {
       private readonly IMongoCollection<Orders> _orders;

public OrdersService(string connectionString, string databaseName)
{
    Console.WriteLine($"ConnectionString in OrdersService: {connectionString}");
    Console.WriteLine($"DatabaseName in OrdersService: {databaseName}");

    var client = new MongoClient(connectionString);
    var database = client.GetDatabase(databaseName);
    _orders = database.GetCollection<Orders>("orders");
}


        public List<Orders> Get() => _orders.Find(orders => true).ToList();

        public Orders Get(string id) => _orders.Find(orders => orders.Id == id).FirstOrDefault();

        public Orders Create(Orders orders)
        {
            _orders.InsertOne(orders);
            return orders;
        }

        public void Update(string id, Orders updatedOrders) => _orders.ReplaceOne(orders => orders.Id == id, updatedOrders);

        public void Delete(Orders ordersForDeletion) => _orders.DeleteOne(orders => orders.Id == ordersForDeletion.Id);

        public void Delete(string id) => _orders.DeleteOne(orders => orders.Id == id);
        
    }
}
