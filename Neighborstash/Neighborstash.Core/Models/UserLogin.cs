using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Neighborstash.Core.Models
{
    public partial class UserLogin
    {
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Id { get; set; }
        [BsonId]
        public string Username { get; set; }
        public string Passwordhash { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Lastlogin { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Createdate { get; set; }
    }

    public partial class NeighborStasher
    {
        [BsonId]
        public string Username { get; set; }
        public List<Stasher> Stashers { get; set; }
    }

    public partial class Stasher
    {
        [BsonId]
        public string Username { get; set; }
        public int Rank { get; set; }
    }
}