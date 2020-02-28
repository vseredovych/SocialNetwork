﻿using DAL.Entities;
using MongoDB.Driver;

namespace DAL.DatabaseConfig
{
    public class MongoContext : IMongoContext
    {
        IMongoDatabase database;

        public MongoContext(IMongoConfiguration mongoConfig)
        {
            var client = new MongoClient(mongoConfig.connectionString);
            database = client.GetDatabase(mongoConfig.databaseName);
        }

        public IMongoCollection<Post> Posts()
        {
            return database.GetCollection<Post>("posts");
        }
    }
}
