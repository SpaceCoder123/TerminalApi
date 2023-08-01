namespace Terminal.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; } = new byte[32];
        public byte[]? PasswordSalt { get; set; } = new byte[32];
        public string VerificationToken { get; set; } = string.Empty;
        public string PasswordResetToken { get; set; } = string.Empty;
        public DateTime VerifiedAt { get; set; }
        public DateTime PasswordResetTokenExpires { get; set; }
    }
}
