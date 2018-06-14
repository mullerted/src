using MongoDB.Bson.Serialization.Attributes;

namespace Neighborstash.Core.Models
{
    public class ServiceRequest
    {
        [BsonId]
        public int Id { get; set; }
        [BsonId]
        public string NeighborUsername { get; set; }
        [BsonId]
        public string StasherUsername { get; set; }
        public int ServicetypeId { get; set; }
        public int ServiceStatusCode { get; set; }
        public  TrackingInfo ItemTrackingInfo { get; set; }
    }


    public class TrackingInfo
    {
        public string TrackingNum { get; set; }
        public string TrackingProvider { get; set; }

    }
}