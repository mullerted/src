using MongoDB.Driver;

namespace Neighborstash.Core.Repositories
{
    public interface INeighborstashContext
    {
        IMongoDatabase Database { get; }
    }
}