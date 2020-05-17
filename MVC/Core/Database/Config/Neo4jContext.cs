using MVC.Core.Entities;
using Neo4jClient;
using System;

namespace MVC.Core.Database.Config
{
    public class Neo4jContext : INeo4jContext
    {
        private readonly IGraphClient _client;

        public Neo4jContext(INeo4jConfiguration neo4JConfig)
        {
            _client = new GraphClient(
                new Uri(neo4JConfig.connectionString),
                neo4JConfig.username,
                neo4JConfig.password
                );            
        }

        public IGraphClient Client
        {
            get
            {
                return _client;
            }
        }
    }
}
