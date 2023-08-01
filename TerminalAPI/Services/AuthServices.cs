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

        public AuthServices(IPasswordService passwordService, DataContext context)
        {
            _context = context;
            _passwordService = passwordService;
        }

        #region RegisterUser
        public async Task<string> RegisterUser(UserDTO request)
        {
            if (_context.Users.Any(u => u.Email == request.Email))
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
                VerificationToken = _passwordService.CreateToken(request),
                PasswordResetTokenExpires = DateTime.Now.AddDays(1),
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.VerificationToken;

        }
        #endregion

        public async Task<string> Login(UserLoginDTO request)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.UserName);
                if (user == null)
                {
                    throw new Exception("No user found");
                }
                if (!_passwordService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    throw new Exception("Password is Incorrect");
                }
                if (user.VerifiedAt == DateTime.MinValue)
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

        public async Task<string> Verify(string token)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
                if (user == null)
                {
                    throw new Exception("No user found with the particular token");
                }
                user.VerifiedAt = DateTime.Now;
                await _context.SaveChangesAsync();

                return "User verified";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> ForgotPassword(string email)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    throw new Exception("No user found with the particular email, Kindly Register");
                }
                UserDTO User1 = new UserDTO();
                User1.Email = email;
                user.PasswordResetToken = _passwordService.CreateToken(User1);
                user.PasswordResetTokenExpires= DateTime.Now.AddMinutes(20);
                await _context.SaveChangesAsync();

                return "You may reset your password";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }   
        
        public async Task<string> ResetPassword(ResetPasswordRequest resetPassword)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == resetPassword.Token);
                if (user == null)
                {
                    throw new Exception("No user found with the particular email, Kindly Register");
                }  
                if (user.PasswordResetTokenExpires <= DateTime.Now)
                {
                    user.PasswordResetTokenExpires = DateTime.MinValue;
                    user.PasswordResetToken = string.Empty;
                    await _context.SaveChangesAsync();
                    throw new Exception("Token has been expired try again!");
                }
                var test = _passwordService.VerifyPasswordHash(resetPassword.Password, user.PasswordHash, user.PasswordSalt);
                if(test)
                {
                    throw new Exception("You cannot enter the old password");
                }
                _passwordService.CreatePasswordHash(resetPassword.Password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.PasswordResetToken = string.Empty;
                user.PasswordResetTokenExpires = DateTime.MinValue; 

                await _context.SaveChangesAsync();

                return "You have successfully reset your password";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }

}
