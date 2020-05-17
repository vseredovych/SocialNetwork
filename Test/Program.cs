using Neo4jClient;
using Neo4jClient.Cypher;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TEst
{


    //public void CreatePerson(Person person)
    //{
    //    _graphClient.Cypher
    //        .Create("(np:Person {newPerson})")
    //        .WithParam("newPerson", person)
    //        .ExecuteWithoutResults();
    //}
    //public void CreatRelationShip(Person whoStartFollow, Person whomFollow)
    //{
    //    _graphClient.Cypher
    //        .Match("(p1:Person {nickname: {p1NickName}})", "(p2:Person {nickname: {p2NickName}})")
    //        .WithParam("p1NickName", whoStartFollow.NickName)
    //        .WithParam("p2NickName", whomFollow.NickName)
    //        .Create("(p1)-[:FOLLOW]->(p2)")
    //        .ExecuteWithoutResults();
    //}
    //public void DeleteRelationShip(Person whoStopFollow,Person whomFollow)
    //{
    //       _graphClient.Cypher
    //         .Match("(p1:Person {nickname: {p1NickName}})-[r:FOLLOW]->(p2:Person {nickname: {p2NickName}})")
    //         .WithParam("p1NickName", whoStopFollow.NickName)
    //         .WithParam("p2NickName", whomFollow.NickName)
    //         .Delete("r")
    //         .ExecuteWithoutResults();
    //}

    public class Person
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "surname")]
        public string Surname { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }

    class Program
    {

        static void Main(string[] args)
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data/"), "neo4j", "superpassword");
            client.Connect();

            var query = client.Cypher
                .Match("path = shortestPath((p1:Person)-[:FRIEND*]->(p2:Person))")
                .Where((Person p1) => p1.Email == "know.nothing@what.why")
                .AndWhere((Person p2) => p2.Email == "victor.seredovich@gmail.com")
                .Return(() => Return.As<IEnumerable<string>>("[n IN nodes(path) | n.email]"));

            var path = query.Results.Single();
            var path_length = path.Count() - 1;
            //Delete
            //client.Cypher
            //    .Match("(person:Person {email: {personEmail}})")
            //    .WithParam("personEmail", "test1")
            //    .Delete("person")
            //    .ExecuteWithoutResults

            //Delete
            //client.Cypher
            //    .Match("(person:Person {email: {personEmail}})")
            //    .WithParam("personEmail", "test1")
            //    .Delete("person")
            //    .ExecuteWithoutResults();

            //Create
            //var person = new Person() { Email = "test2@gmail.com", Name = "A1", Surname = "B1" };
            //client.Cypher
            //    .Create("(np:Person {newPerson})")
            //    .WithParam("newPerson", person)
            //    .ExecuteWithoutResults();

            //Create relationship
            //client.Cypher
            //    .Match("(p1:Person {email: {p1Email}})", "(p2:Person {email: {p2Email}})")
            //    .WithParam("p1Email", "test1@gmail.com")
            //    .WithParam("p2Email", "test2@gmail.com")
            //    .Create("(p1)-[:FOLLOW]->(p2)")
            //    .ExecuteWithoutResults();


            // Delete relationship
            //client.Cypher
            //  .Match("(p1:Person {email: {p1Email}})-[r:FOLLOW]->(p2:Person {email: {p2Email}})")
            //  .WithParam("p1Email", "test1@gmail.com")
            //  .WithParam("p2Email", "test2@gmail.com")
            //  .Delete("r")
            //  .ExecuteWithoutResults();
        }
    }
}
