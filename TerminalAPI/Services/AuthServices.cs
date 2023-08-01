using Microsoft.EntityFrameworkCore;
using Terminal.DTOs;
using Terminal.JWT.Services;
using Terminal.Models;
using TerminalAPI.Data;

namespace TerminalAPI.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IPasswordService _passwordService;
        private readonly DataContext _context;
        //public static User user = new User();

        public AuthServices(IPasswordService passwordService, DataContext context)
        {
            _context= context;
            _passwordService = passwordService;
        }

        #region RegisterUser
        public async Task<User> RegisterUser(UserDTO request)
        {
            if(_context.Users.Any(u=>u.Email == request.Email))
            {
                throw new Exception("Email already exists");
            }

            if (_context.Users.Any(u => u.Username == request.Username))
            {
                throw new Exception("Username already exists");
            }

            _passwordService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User()
            {
                Email = request.Email,
                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = _passwordService.CreateToken(request)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;

        }
        #endregion

        public async Task<string> Login(UserLoginDTO request)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u=>u.Email == request.Email);
                if(user == null) 
                {
                    throw new Exception("No user found");
                }
                if(!_passwordService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    throw new Exception("Password is Incorrect");
                }
                if(user.VerifiedAt == null)
                {
                    throw new Exception("Not Verified");
                }
                return "Welcome back";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
