using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Neighborstash.Core.Models
{
    public partial class UserReview
    {
        public string ReviewerId { get; set; }

        public string RevieweeId { get; set; }

        public string ServiceprovidedId { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Reviewdate { get; set; }

        public int Reviewpoint { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public string Reviewtext { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Lastupdate { get; set; }
    }
}