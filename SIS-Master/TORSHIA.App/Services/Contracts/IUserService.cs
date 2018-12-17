using TORSHIA.Domain;

namespace TORSHIA.Services.Contracts
{
    public interface IUserService
    {
        User RegisterUser(string username, string password, string confirmPassword, string email);
        User LoginUser(string username, string password);
    }
}