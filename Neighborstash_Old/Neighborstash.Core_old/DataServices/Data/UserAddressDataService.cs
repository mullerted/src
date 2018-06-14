using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Neighborstash.Core.Contracts;
using Neighborstash.Core.Models;

namespace Neighborstash.Core.Contracts
{
    public class UserAddressDataService : IUserAddressDataService
    {
        private readonly IUserAddressRepository _userAddressRepository;

        public UserAddressDataService(IUserAddressRepository userAddressRepository)
        {
            _userAddressRepository = userAddressRepository;
        }

        public Task<IEnumerable<UserAddress>> GetAllUserAddresses()
        {
            return _userAddressRepository.GetAddressForAllUsers();
        }

        public Task<IEnumerable<UserAddress>> GetUserAddress(string username)
        {
            return _userAddressRepository.GetAddressForUser(username);
        }
    }
}
