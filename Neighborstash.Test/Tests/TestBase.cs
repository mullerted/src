using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using Neighborstash.Core.Repositories;

namespace Neighborstash.Test
{
    public class TestBase
    {
        public NeighborstashContext NSContext { get; set; }
        public TestBase()
        {
           
            JsonWriterSettings.Defaults.Indent = true;
        }


        protected async Task<BsonDocument> GetServerInfoAsync(NeighborstashContext context)
        {
            var bi = new BsonDocument("buildinfo", 1);
            var buildInfo = await context.Database.RunCommandAsync<BsonDocument>(bi);
            return buildInfo;
        }
        

        protected string ConvertToSha256(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }

}