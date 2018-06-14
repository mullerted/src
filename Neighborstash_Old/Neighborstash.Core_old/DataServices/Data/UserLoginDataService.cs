using System;
using System.Collections.Generic;
using System.Text;

namespace Neighborstash.Core.Contracts
{
    public class UserLoginDataService : IUserLoginDataService
    {
        private readonly IUserLoginRepository _userLoginRepository;

        public UserLoginDataService(IUserLoginRepository userLoginRepository)
        {
            _userLoginRepository = userLoginRepository;
        }
    }
}
