using System.Linq;
using TORSHIA.Data;
using TORSHIA.Domain;
using TORSHIA.Services.Contracts;

namespace TORSHIA.Services
{
    public class UserService : IUserService
    {
        private readonly TorshiaContext context;

        public UserService(TorshiaContext context)
        {
            this.context = context;
        }

        public User RegisterUser(string username, string password, string confirmPassword, string email)
        {
            UserRole role = new UserRole();
            if (!context.Users.Any())
            {
                role = context.UserRoles.FirstOrDefault(r => r.Name == "Admin");
            }
            else
            {
                role = context.UserRoles.FirstOrDefault(r => r.Name == "User");
            }

            User user = new User()
            {
                Username = username,
                Password = password,
                Email = email,
                Role = role,
            };

            context.Users.Add(user);
            context.SaveChanges();

            return user;
        }

        public User LoginUser(string username, string password)
        {
            return this.context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}