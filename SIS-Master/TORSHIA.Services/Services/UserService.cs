using TORSHIA.Data;
using TORSHIA.Services.Services.Contracts;

namespace TORSHIA.Services.Services
{
    public class UserService : IUserService
    {
        private readonly TorshiaContext context = new TorshiaContext();
        public void RegisterUser(string username, string password, string confirmPassword, string email)
        {
            throw new System.NotImplementedException();
        }

        public void LoginUser(string username, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}