using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Neighborstash.Core.Models
{
    [BsonIgnoreExtraElements]
    public class ZipCode
    {
        [BsonId]
        public string Id { get; set; }
        [BsonElement("city")]
        public string City { get; set; }
        [BsonElement("state")]
        public string State { get; set; }
    }
}
