using System.Security.Cryptography;
using System.Text;

namespace Application.Helper
{
    public static class SecurityHelper
    {
        public static string HashPassword(string password, string salt)
        {
            int iterations = 100000;
            int keySize = 32;
            var saltBytes = Encoding.UTF8.GetBytes(salt);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, iterations, HashAlgorithmName.SHA256);
            byte[] hashBytes = pbkdf2.GetBytes(keySize);
            return Convert.ToBase64String(hashBytes);
        }

        public static string GenerateSalt()
            => StringHelper.GeneratorRandomStringBase64(16);
        public static bool VerifyPassword(string password, string salt, string passwordHash)
            => HashPassword(password, salt) == passwordHash;
    }
}