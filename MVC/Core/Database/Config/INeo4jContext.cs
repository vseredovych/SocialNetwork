using MongoDB.Driver;
using MVC.Core.Entities;
using Neo4jClient;

namespace MVC.Core.Database.Config
{
    public interface INeo4jContext
    {
        IGraphClient Client { get; }
    }
}