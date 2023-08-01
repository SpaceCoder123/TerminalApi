using Terminal.DTOs;
using Terminal.Models;

namespace TerminalAPI.Services
{
    public interface IAuthServices
    {
        public Task<User> RegisterUser (UserDTO request);

        public Task<string> Login(UserLoginDTO request);
    }
}
