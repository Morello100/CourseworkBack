using mongodb_dotnet_example.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System;

namespace mongodb_dotnet_example.Services
{
    public class UsersService
    {
        private readonly IMongoCollection<User> _users;

        public UsersService(string connectionString, string databaseName)
        {
            Console.WriteLine($"ConnectionString in UsersService: {connectionString}");
            Console.WriteLine($"DatabaseName in UsersService: {databaseName}");

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _users = database.GetCollection<User>("users");
        }

        public List<User> Get() => _users.Find(user => true).ToList();

        public User Get(string id) => _users.Find(user => user.Id == id).FirstOrDefault();

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User updatedUser) => _users.ReplaceOne(user => user.Id == id, updatedUser);

        public void Delete(User userForDeletion) => _users.DeleteOne(user => user.Id == userForDeletion.Id);

        public void Delete(string id) => _users.DeleteOne(user => user.Id == id);

        public User FindUserByEmail(string email)
        {
            return _users.Find(user => user.Email == email).FirstOrDefault();
        }
    }
}
