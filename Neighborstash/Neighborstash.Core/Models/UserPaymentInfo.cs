using MongoDB.Bson.Serialization.Attributes;

namespace Neighborstash.Core.Models
{
    public class UserPaymentInfo
    {
        [BsonId]
        public string Username { get; set; }
        public string AccountNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CCode { get; set; }
        public string Name { get; set; }
        public AddressDetail PaymentAddress { get; set; }
    }
}