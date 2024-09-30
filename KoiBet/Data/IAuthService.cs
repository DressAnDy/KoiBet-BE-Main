using KoiBet.Entities;
using System.Threading.Tasks;

namespace KoiBet.Services
{
    public interface IAuthService
    {
        // Login method
        Task<Users> Login(string username, string password);

        // Register method
        Task<Users> Register(Users user);

        //UserExists method
        Task<bool> UserExists(string username);

        // Changepassword method
        Task<bool> ChangePassword(string username, string currentPassword, string newPassword);

        //Forgotpassword method
        Task<bool> ForgotPassword(string email, string newPassword, string confirmPassword);
    }
}
