namespace TORSHIA.Services.Services.Contracts
{
    public interface IUserService
    {
        void RegisterUser(string username, string password, string confirmPassword, string email);
        void LoginUser(string username, string password);
    }
}