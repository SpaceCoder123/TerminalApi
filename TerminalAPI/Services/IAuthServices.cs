using Terminal.DTOs;
using Terminal.Models;

namespace TerminalAPI.Services
{
    public interface IAuthServices
    {
        public Task<string> RegisterUser (UserDTO request);
        public Task<string> Login(UserLoginDTO request);
        public Task<string> Verify(string token);
        public Task<string> ForgotPassword(string email);
        public Task<string> ResetPassword(ResetPasswordRequest resetPassword);
    }
}
