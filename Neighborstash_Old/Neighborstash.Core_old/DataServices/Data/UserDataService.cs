using Neighborstash.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Neighborstash.Core.DataServices
{
    public class UserDataService : IUserDataService
    {
        private readonly IUserRepository _userRepository;
        public UserDataService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

    }
}
