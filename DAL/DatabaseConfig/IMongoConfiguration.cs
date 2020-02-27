using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DatabaseConfig
{
    public interface IMongoConfiguration
    {
        string connectionString { get; set; }
        string databaseName { get; set; }
    }
}
