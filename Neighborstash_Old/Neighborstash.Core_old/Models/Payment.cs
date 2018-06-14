using MongoDB.Bson.Serialization.Attributes;

namespace Neighborstash.Core.Models
{
    public class Payment
    {
        [BsonId]
        public string NeighborUsername { get; set; }
        [BsonId]
        public string StasherUsername { get; set; }
        public int AccountTypeId { get; set; }
        public int PaymentMethodCode { get; set; }
        public int PaymentStatusCode { get; set; }
        [BsonId]
        public  int ServiceRequestId { get; set; }
        public string ServiceLocation { get; set; }

    }
}