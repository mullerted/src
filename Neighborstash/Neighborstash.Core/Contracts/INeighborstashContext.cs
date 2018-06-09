using MongoDB.Driver;
using Neighborstash.Core.Models;
using Neighborstash.Core.ViewModels;
using UserLogin = Neighborstash.Core.Models.UserLogin;

namespace Neighborstash.Core.Contracts
{
    public interface INeighborstashContext
    {
        IMongoDatabase Database { get; }
        IMongoCollection<User> Users { get;  }

        IMongoCollection<UserLogin> UserLogins { get;  }


        IMongoCollection<AccountType> AccountTypes { get;  }


        IMongoCollection<UserReview> UserReviews { get;  }

        IMongoCollection<UserAddress> UserAddresses { get;  }

        IMongoCollection<SecurityQeustion> UserSecurityQuestions { get;  }
    }
}