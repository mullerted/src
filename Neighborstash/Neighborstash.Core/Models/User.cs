using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Neighborstash.Core.Models
{
    public class User
    {
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Id { get; set; }

        [BsonId] public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public List<Phone> Phones { get; set; }

        // public AddressElement[] Addresses { get; set; }

        public List<SocIdentity> Identities { get; set; }

        public int UserType { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Createdate { get; set; }

        [BsonRepresentation(BsonType.Boolean)] public bool IsActive { get; set; }

        public string Picture { get; set; }

        public string Locale { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Lastupdate { get; set; }
    }
}