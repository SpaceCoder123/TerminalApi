using Terminal.DTOs;
using Terminal.Models;

namespace TerminalAPI.Services
{
    public interface IAuthServices
    {
        public User RegisterUser (UserDTO request);

        public string Login(UserDTO request);
    }
}
