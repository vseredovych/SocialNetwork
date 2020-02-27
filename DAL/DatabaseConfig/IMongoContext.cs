using DAL.Entities;
using DAL.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DatabaseConfig
{
    public interface IMongoContext
    {
        IMongoCollection<Post> Posts();
    }
}
