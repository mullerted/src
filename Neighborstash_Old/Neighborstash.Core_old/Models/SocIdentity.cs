using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Neighborstash.Core.Models
{
    public partial class SocIdentity
    {
        [BsonId]
        public string UserId { get; set; }
        public string Provider { get; set; }
        public string Connection { get; set; }
        [BsonRepresentation(BsonType.Boolean)] public bool IsSocial { get; set; }
    }
}