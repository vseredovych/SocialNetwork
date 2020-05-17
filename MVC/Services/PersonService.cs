using AutoMapper;
using MongoDB.Driver;
using MVC.Core.Database.Config;
using MVC.Core.Entities;
using MVC.Interfaces;
using Neo4jClient.Cypher;
using System.Collections.Generic;
using System.Linq;

namespace MVC.Services
{
    public class PersonService : IPersonService
    {
        private readonly INeo4jContext _context;
        private readonly IMapper _mapper;

        public PersonService(INeo4jContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void CreatePerson(User user)
        {
            var person = _mapper.Map<Person>(user);
            _context.Client.Connect();
            _context
                .Client
                .Cypher
                .Create("(np:Person {newPerson})")
                .WithParam("newPerson", person)
                .ExecuteWithoutResults();

            _context.Client.Dispose();
        }

        public void DeletePerson(string email)
        {
            _context.Client.Connect();
            _context
                 .Client
                 .Cypher
                 .Match("(person:Person {email: {personEmail}})")
                 .WithParam("personEmail", email)
                 .Delete("person")
                 .ExecuteWithoutResults();

            _context.Client.Dispose();
        }

        public void CreateRelationship(string email1, string email2)
        {
            _context.Client.Connect();
            _context
                .Client
                .Cypher
                .Match("(p1:Person {email: {p1Email}})", "(p2:Person {email: {p2Email}})")
                .WithParam("p1Email", email1)
                .WithParam("p2Email", email2)
                .Create("(p1)-[:FRIEND]->(p2)")
                .ExecuteWithoutResults();

            _context.Client.Dispose();
        }

        public void DeleteRelationship(string email1, string email2)
        {
            _context.Client.Connect();
            _context
                .Client
                .Cypher
                .Match("(p1:Person {email: {p1Email}})-[r:FRIEND]->(p2:Person {email: {p2Email}})")
                .WithParam("p1Email", email1)
                .WithParam("p2Email", email2)
                .Delete("r")
                .ExecuteWithoutResults();

            _context.Client.Dispose();
        }

        public int GetShortestPathLength(string email1, string email2)
        {
            if (email1 == email2)
            {
                return 0;
            }

            _context.Client.Connect();
            var query = _context
                 .Client
                 .Cypher
                 .Match("path = shortestPath((p1:Person)-[:FRIEND*]->(p2:Person))")
                 .Where((Person p1) => p1.Email == email1)
                 .AndWhere((Person p2) => p2.Email == email2)
                 .Return(() => Return.As<IEnumerable<string>>("[n IN nodes(path) | n.email]"));

            var path = query.Results.Single();
            var pathLength = path.Count() - 1;

            _context.Client.Dispose();
            return pathLength;
        }
    }
}
