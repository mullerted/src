using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Neighborstash.Core.Models
{
    //public class AddressElement
    public class UserAddress
    {
        [BsonId]
        public string Username { get; set; }
        public string Location { get; set; }

        public AddressDetail Address { get; set; }

        public Geo Geo { get; set; }
    }

    public class AddressDetail
    {
        public string Street { get; set; }

        public string Addr1 { get; set; }

        public string Addr2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Pcode { get; set; }
    }

    public class Geo
    {
        public Geo()
        {
            Coord = new double[2];
        }

        public string Type { get; set; }


        public double[] Coord { get; set; }
    }

}
