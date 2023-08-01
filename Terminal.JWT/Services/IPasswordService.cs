using Terminal.Models;

namespace Terminal.JWT.Services
{
    public interface IPasswordService
    {
        public void CreatePasswordHash (string username, out byte[] passwordHash, out byte[] passwordSalt);

        public bool VerifyPasswordHash(string username, byte[] passwordHash, byte[] passwordSalt);

        public string CreateToken(User user);

        //public RefreshToken GenerateRefreshToken();

    }
}
