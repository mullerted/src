using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Neighborstash.Core.Contracts
{
    /// <summary>
    /// combines data from user profile model and useraddress to
    /// return UserDemographics viewmodel
    /// </summary>
    public class UserDemographicsDataService : IUserDemographicsDataService
    {

        private readonly IUserAddressRepository _userAddressRepository;
        private readonly IUserRepository _userRepository;

        public UserDemographicsDataService(IUserRepository userRepository, IUserAddressRepository userAddressRepository )
        {
            _userRepository = userRepository;
            _userAddressRepository = userAddressRepository;
        }
        //public async Task<List<UserDemographics>> GetUserDemographics()
        //{
        //    // _userRepository.GetUser() 
        //    // _userAddressRepositor.GetUserAddress("username");
        //    // combine results into one viewmodel and return 
        //    //return await ...
        //}
    }
}
