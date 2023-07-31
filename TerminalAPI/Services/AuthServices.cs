using Terminal.DTOs;
using Terminal.JWT.Services;
using Terminal.Models;

namespace TerminalAPI.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IPasswordService _passwordService;
        public static User user = new User();

        public AuthServices(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        #region RegisterUser
        public User RegisterUser(UserDTO request)
        {
            if (request.Username != null && request.Password != null)
            {
                if (request.Username.Length > 0 && request.Password.Length > 0)
                {
                    _passwordService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    user.PasswordSalt = passwordHash;
                    user.PasswordHash = passwordSalt;
                    user.Username = request.Username;
                    return user;
                }
                else
                {
                    throw new Exception("User name or password cannot be empty");
                }

            }
            else
            {
                throw new Exception("User name or password cannot be null");
            }

        }
        #endregion

        public bool ValidateCredentails(UserDTO request)
        {
            if(request.Username == user.Username)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string Login(UserDTO request)
        {
            if(!ValidateCredentails(request))
            {
                throw new Exception("The user is not found, kindly register");
            }
            bool verifyPasswordHash = _passwordService.VerifyPasswordHash(request.Username, user.PasswordHash, user.PasswordSalt);
            if (!verifyPasswordHash)
            {
                throw new Exception("The user password is wrong");
            }
            return "My Crazy Token";

        }
    }

}
