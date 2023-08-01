﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Terminal.DTOs;
using Terminal.Models;

namespace Terminal.JWT.Services
{
    public class PasswordPropertiesService : IPasswordService
    {
        private readonly IConfiguration _configuration;
        public PasswordPropertiesService(IConfiguration Config)
        {
            _configuration = Config;
        }
        /// <summary>
        /// Create Password Hash Method which takes the password and returns it into password salt and password hash using SHA512 encryption algorithm
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordSalt"></param>
        /// <param name="passwordHash"></param>
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                bool check = computeHash.SequenceEqual(passwordHash);
                return check;
            }
        }

        public string CreateToken(UserDTO user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim (ClaimTypes.Name, user.Username)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires= DateTime.Now.AddDays(1),
                Created = DateTime.Now
            };
            return refreshToken;
        }

        //public void SetRefreshToken(RefreshToken refreshToken)
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = refreshToken.Expires
        //    };
        //    Response.Cookies.Append("RefreshToken", refreshToken.Token, cookieOptions);
        //}
    }
}
