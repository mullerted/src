// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var neighborstash = Neighborstash.FromJson(jsonString);

namespace Neighborstash.Core.ViewModels
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Neighborstash
    {
        [JsonProperty("Users")]
        public Users Users { get; set; }

        [JsonProperty("userLogin")]
        public UserLogin UserLogin { get; set; }

        [JsonProperty("reviews")]
        public Reviews Reviews { get; set; }

        [JsonProperty("accountType")]
        public AccountType AccountType { get; set; }
    }

    public partial class AccountType
    {
        [JsonProperty("typename")]
        public string Typename { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public partial class Reviews
    {
        [JsonProperty("reviewer_id")]
        public string ReviewerId { get; set; }

        [JsonProperty("reviewee_id")]
        public string RevieweeId { get; set; }

        [JsonProperty("serviceprovided_id")]
        public string ServiceprovidedId { get; set; }

        [JsonProperty("reviewdate")]
        public string Reviewdate { get; set; }

        [JsonProperty("reviewpoint")]
        public string Reviewpoint { get; set; }

        [JsonProperty("reviewtext")]
        public string Reviewtext { get; set; }

        [JsonProperty("lastupdate")]
        public string Lastupdate { get; set; }
    }

    public partial class UserLogin
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("passwordhash")]
        public string Passwordhash { get; set; }

        [JsonProperty("lastlogin")]
        public string Lastlogin { get; set; }

        [JsonProperty("createdate")]
        public string Createdate { get; set; }
    }

    public partial class Users
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public Phone[] Phone { get; set; }

        [JsonProperty("addresses")]
        public AddressElement[] Addresses { get; set; }

        [JsonProperty("twitter-uid")]
        public string TwitterUid { get; set; }

        [JsonProperty("facebook-uid")]
        public string FacebookUid { get; set; }

        [JsonProperty("accountType")]
        public string AccountType { get; set; }

        [JsonProperty("createdate")]
        public string Createdate { get; set; }

        [JsonProperty("isactive")]
        public string Isactive { get; set; }

        [JsonProperty("lastupdate")]
        public string Lastupdate { get; set; }
    }

    public partial class AddressElement
    {
        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("address")]
        public AddressDetail Address { get; set; }

        [JsonProperty("geo")]
        public Geo Geo { get; set; }
    }

    public partial class AddressDetail
    {
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("addr1")]
        public string Addr1 { get; set; }

        [JsonProperty("addr2")]
        public string Addr2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("pcode")]
        public string Pcode { get; set; }
    }

    public partial class Geo
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coord")]
        public double[] Coord { get; set; }
    }

    public partial class Phone
    {
      
        public string Type { get; set; }

        public string Number { get; set; }
    }

    //public partial class Neighborstash
    //{
    //    public static Neighborstash FromJson(string json) => JsonConvert.DeserializeObject<Neighborstash>(json, QuickType.Converter.Settings);
    //}

    //public static class Serialize
    //{
    //    public static string ToJson(this Neighborstash self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    //}

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
