using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Neighborstash.Core.Contracts;
using Neighborstash.Core.Models;

namespace Neighborstash.Core.Repositories
{
    public class BaseRepository
    {
        protected readonly INeighborstashContext _nsContext;

        public BaseRepository(INeighborstashContext nsContext)
        {
            _nsContext = nsContext;
        }
    }
    public class UserAddressRepository : BaseRepository,IUserAddressRepository
    {
        
        public UserAddressRepository(INeighborstashContext neighborstashContext) : base(neighborstashContext)
        {
           
        }

        public async Task<IEnumerable<UserAddress>> GetAddressForUser(string username)
        {
            var filterDefinition = Builders<UserAddress>.Filter.Empty;
            filterDefinition &= Builders<UserAddress>.Filter.Eq(u => u.Username, username);
            
            var result = await _nsContext.UserAddresses.FindAsync(filterDefinition);
            return result.ToEnumerable();
            //return await Task.FromResult(result.ToEnumerable());


            return result.ToEnumerable();
        }

    }
}