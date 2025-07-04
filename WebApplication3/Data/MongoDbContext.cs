﻿using MongoDB.Driver;
using WebApplication3.Models;

namespace WebApplication3.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDb:ConnectionString"]);
            _database = client.GetDatabase(configuration["MongoDb:DatabaseName"]);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    }
}
