using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using Neighborstash.Core.Repositories;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Neighborstash.Test
{
    [TestFixture]
    public class MongoDbTest : TestBase
    {
        [TestCase("localhost", 27017)]
        [ExpectedException(typeof(ConnectionToDbException))]
        public void ShouldEstablishConnectionToMongoDB(string host, int portNum)
        {
            var client = new MongoClient($"mongodb://{host}:{portNum}");

            Assert.IsNotNull(client);

            // or, to connect to a replica set, with auto-discovery of the primary, supply a seed list of members
            //var client = new MongoClient("mongodb://localhost:27017,localhost:27018,localhost:27019");
        }

        [TestCase("localhost", 27017, "Neighborstash")]
        [ExpectedException(typeof(ConnectionToDbException))]
        public void ShouldEstablishConnectionAndGetDatabaseUsingContext(string host, int portNum, string databaseName)
        {
            var dbSettings = new NeighbostashDbSettings
            {
                ConnectionString = $"mongodb://{host}:{portNum}",
                DatabaseName = databaseName
            };

            INeighborstashContext context = new NeighborstashContext(dbSettings);
            var database = context.Database;


            Console.WriteLine($"DBNamespace: {database.DatabaseNamespace}");

            Assert.IsNotNull(database);

            // or, to connect to a replica set, with auto-discovery of the primary, supply a seed list of members
            //var client = new MongoClient("mongodb://localhost:27017,localhost:27018,localhost:27019");
        }

        [TestCase("localhost", 27017, "Neighborstash")]
        [ExpectedException(typeof(ConnectionToDbException))]
        public void ShouldGetNeighborStashDatabase(string host, int portNum, string databaseName)
        {
            var client = new MongoClient($"mongodb://{host}:{portNum}");
            var database = client.GetDatabase(databaseName);

            Assert.IsNotNull(database);

            // or, to connect to a replica set, with auto-discovery of the primary, supply a seed list of members
            //var client = new MongoClient("mongodb://localhost:27017,localhost:27018,localhost:27019");
        }

        [TestCase("localhost", 27017, "Neighborstash", "User")]
        [ExpectedException(typeof(ConnectionToDbException))]
        public void ShouldGetNeighborStashUserCollection(string host, int portNum, string databaseName,
            string collectionName)
        {
            var client = new MongoClient($"mongodb://{host}:{portNum}");
            var database = client.GetDatabase("Neighborstash");
            var userCollection = database.GetCollection<BsonDocument>("user");


            Assert.IsNotNull(userCollection);

            // or, to connect to a replica set, with auto-discovery of the primary, supply a seed list of members
            //var client = new MongoClient("mongodb://localhost:27017,localhost:27018,localhost:27019");
        }


        [TestCase("localhost", 27017, "Neighborstash", "User", 3)]
        [ExpectedException(typeof(ConnectionToDbException))]
        public void ShouldHaveUsers(string host, int portNum, string databaseName, string collectionName, int cnt)
        {
            var client = new MongoClient($"mongodb://{host}:{portNum}");
            var database = client.GetDatabase("Neighborstash");
            var userCollection = database.GetCollection<BsonDocument>("user");
            var count = userCollection.Count(new BsonDocument());


            Assert.IsTrue(count == cnt);

            // or, to connect to a replica set, with auto-discovery of the primary, supply a seed list of members
            //var client = new MongoClient("mongodb://localhost:27017,localhost:27018,localhost:27019");
        }

        [TestCase("localhost", 27017, "Neighborstash", "User")]
        [ExpectedException(typeof(ConnectionToDbException))]
        public void ShouldInsertUser(string host, int portNum, string databaseName, string collectionName)
        {
            var client = new MongoClient($"mongodb://{host}:{portNum}");
            var database = client.GetDatabase("Neighborstash");
            var userCollection = database.GetCollection<BsonDocument>("user");

            var document = new BsonDocument
            {
                {"name", "MongoDB"},
                {"type", "Database"},
                {"count", 1},
                {
                    "info", new BsonDocument
                    {
                        {"x", 203},
                        {"y", 102}
                    }
                }
            };

            Assert.IsNotNull(userCollection);

            // or, to connect to a replica set, with auto-discovery of the primary, supply a seed list of members
            //var client = new MongoClient("mongodb://localhost:27017,localhost:27018,localhost:27019");
        }

        [Test]
        public void CreatingBsonDoc()
        {
            var doc = new BsonDocument
            {
                {"a", 1},
                {
                    "b", new BsonArray
                    {
                        new BsonDocument("c", 1)
                    }
                }
            };
        }


        [Test]
        public void shouldGenerateObjectId()
        {
            var objectId = ObjectId.GenerateNewId();
            Console.WriteLine($"{objectId.ToString()}");
            Console.WriteLine($"{objectId.Increment.ToString()}");
            NUnit.Framework.Assert.IsNotNull(objectId);
        }
    }
}