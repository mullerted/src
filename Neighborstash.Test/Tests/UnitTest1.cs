using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Neighborstash.Core.Models;
using Neighborstash.Core.Contracts;
using Neighborstash.Core.Repositories;
using Neighborstash.Core.ViewModels;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Neighborstash.Test.Tests
{
    [TestClass]
    public class LinqAggregate : TestBase
    {
        [TestCase("localhost", 27017, "Neighborstash")]
        [ExpectedException(typeof(ConnectionToDbException))]
        public async Task ShouldDoAggregateVaiLinq(string hostname, int portNum, string databaseName)
        {
            NSContext = new NeighborstashContext(new NeighbostashDbSettings
            {
                ConnectionString = $"mongodb://{hostname}:{portNum}",
                DatabaseName = databaseName
            });

            var filter = new UserFilter
            {
                Usertype = 1  //stahser;
            };

            //var r = await NSContext.Users.AsQueryable()
            //var r = await FilterUsers(filter)
            //    .Select(u => new UserViewModel
            //    {
            //        UserName = u.Username,
            //        UserEmail = u.Email

            //    })
            //    .OrderBy(u => u.UserName)
            //    .ThenByDescending(u => u.LastName)
            //    .ToListAsync();

            var dist = RunAggregationFluent(NSContext.Users);
            foreach (var u in dist)
            {
                Console.WriteLine($"{u}");
            }

            var dist2 = RunLinq(NSContext.Users);
            foreach (var u in dist2)
            {
                Console.WriteLine($"{u}");
            }

            var zips = JoinWithLookup();
            foreach (var zip in zips)
            {
                 Console.WriteLine(zip);
            }
        }


        private IMongoQueryable<User> FilterUsers(UserFilter filter)
        {
            IMongoQueryable<User> users = NSContext.Users.AsQueryable();
           return users.Where(u => u.UserType == filter.Usertype);
        }

        private IEnumerable RunAggregationFluent(IMongoCollection<User> users)
        {
            var distribution = users.Aggregate()
                .Project(r => new
                {
                    r.Lastname,
                    NameRange = r.Lastname.Length - r.Lastname.Length % 3 //GetNameRange(r)
                })
                .Group(r => r.NameRange, g => new {GroupNameRange = g.Key, count = g.Count()})
                .SortBy(r => r.GroupNameRange)
                .ToList();
            return distribution;

        }

        private IEnumerable RunLinq(IMongoCollection<User> users)
        {
            var distribution = users.AsQueryable()
                .Select(r => new 
                {
                    NameRange = r.Lastname.Length - r.Lastname.Length % 3 //GetNameRange(r)
                })
                .GroupBy(r=>r.NameRange)
                .Select(g => new {GroupNameRange = g.Key, Count=g.Count()})
                .OrderBy(r => r.GroupNameRange)
                .ToList();
            return distribution;

        }

        private static int GetNameRange(User r)
        {
            var stratingCharVal = (int) Convert.ToChar(r.Lastname.Substring(1, 1));
            var modVal = stratingCharVal % (int) 'M';
            return (stratingCharVal - modVal);
        }


        private IEnumerable JoinPreLookup()
        {
            var usersAddresses = NSContext.UserAddresses.Find(new BsonDocument()).ToList();
            var userZips = usersAddresses.Select(u => u.Address.Pcode).Distinct().ToArray();

            var zipsById = NSContext.Database.GetCollection<ZipCode>("zips")
                .Find(z => userZips.Contains(z.Id))
                .ToList()
                .ToDictionary(d => d.Id);

            var report = usersAddresses.Select(
                u=> new {
                    Username =u.Username,
                    zipCode = u.Address.Pcode != null && zipsById.ContainsKey(u.Address.Pcode) ? zipsById[u.Address.Pcode] : null,
                    Location =u.Location}
            );

            return report.ToJson();
        }

        private IEnumerable JoinWithLookup()
        {
            var report = NSContext.UserAddresses
                .Aggregate()
                .Lookup<UserAddress, ZipCode, UsersWithZipCodes>(
                    NSContext.Database.GetCollection<ZipCode>("zips"),
                    r =>r.Address.Pcode,
                    z=>z.Id,
                    d => d.ZipCodes
                ).ToList();

            return report.ToJson();
        }
    }


    public class UserFilter
    {
        public string Username { get; set; }
        public int Usertype { get; set; }
    }

    public class UsersWithZipCodes : UserAddress
    {
        public ZipCode[] ZipCodes { get; set; }
    }





}
