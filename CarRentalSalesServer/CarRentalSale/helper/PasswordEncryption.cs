using System.Security.Cryptography;

namespace CarRentalSale.helper
{
    public class PasswordEncryption
    {
        public string? PasswordSalt { get; set; }
        public required string Password { get; set; }

        public void EncryptionPass()
        {
            var salt = GenerateSalt();
            var hashBytes = new Rfc2898DeriveBytes(
                Password,
                salt,
                100_000,
                HashAlgorithmName.SHA256
            ).GetBytes(32);

            PasswordSalt = Convert.ToBase64String(salt);
            Password = Convert.ToBase64String(hashBytes);
        }

        public void EncryptionPass(string base64Salt)
        {
            var salt = Convert.FromBase64String(base64Salt); 

            var hashBytes = new Rfc2898DeriveBytes(
                Password,
                salt,
                100_000,
                HashAlgorithmName.SHA256
            ).GetBytes(32);

            PasswordSalt = base64Salt; 
            Password = Convert.ToBase64String(hashBytes);
        }

        private byte[] GenerateSalt()
        {
            var salt = new byte[16];
            RandomNumberGenerator.Fill(salt);
            return salt;

        }
    }
}