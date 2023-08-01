using Terminal.DTOs;

namespace Terminal.JWT.Services
{
    public interface IPasswordService
    {
        public void CreatePasswordHash (string username, out byte[] passwordHash, out byte[] passwordSalt);

        public bool VerifyPasswordHash(string username, byte[] passwordHash, byte[] passwordSalt);

        public string CreateToken(UserDTO user);

        //public RefreshToken GenerateRefreshToken();

    }
}
