using System.Collections.Generic;
using System.Threading.Tasks;
using Neighborstash.Core.Models;

namespace Neighborstash.Core.Contracts
{
    public interface IUserAddressRepository
    {
        Task<IEnumerable<UserAddress>> GetAddressForUser(string username);
        Task<IEnumerable<UserAddress>> GetAddressForAllUsers();
    }
}