using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Neighborstash.Core.Models;
using Neighborstash.Core.ViewModels;
using UserLogin = Neighborstash.Core.Models.UserLogin;

namespace Neighborstash.Core.Repositories
{
    public class NeighbostashDbSettings : INeighbostashDbSettings
    {
        public string HostName { get; set; }
        public int PortNumber { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public class NeighborstashContext : INeighborstashContext
    {
        private readonly GridFSBucket _imagesBucket;

        public NeighborstashContext(IMongoDatabase database)
        {
            Database = database;
        }

        public NeighborstashContext(INeighbostashDbSettings neighbostashDbSettings)
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(neighbostashDbSettings.ConnectionString));
            if (settings == null) return;
            //settings.WriteConcern.Journal = true;
            // wire-up
            settings.ClusterConfigurator = builder => builder.Subscribe(new Log4NetMongoEvents());

            var client = new MongoClient(settings);
            Database = client.GetDatabase(neighbostashDbSettings.DatabaseName);
            _imagesBucket = new GridFSBucket(Database);
            //var collection = Database.GetCollection<User>();
        }

        public IMongoCollection<User> Users => Database.GetCollection<User>("user");

        public IMongoCollection<UserLogin> UserLogins => Database.GetCollection<UserLogin>("userlogin");

        public IMongoCollection<AccountType> AccountTypes => Database.GetCollection<AccountType>("accountType");

        public IMongoCollection<UserReview> UserReviews => Database.GetCollection<UserReview>("userReview");

        public IMongoCollection<UserAddress> UserAddresses => Database.GetCollection<UserAddress>("userAddress");

        public IMongoCollection<SecurityQeustion> UserSecurityQuestions =>
            Database.GetCollection<SecurityQeustion>("SecurityQuestion");

        public IMongoDatabase Database { get; }


        public async Task<ObjectId> StoreImageAsync(string filename)
        {
            var options = new GridFSUploadOptions
            {
                Metadata = new BsonDocument("contentType", "image/jpeg")
            };
            using (var fs = new FileStream(filename, FileMode.Open))
            {
                var imageId = await _imagesBucket
                    .UploadFromStreamAsync(filename, fs, options);
                return imageId;
            }
        }
    }
}