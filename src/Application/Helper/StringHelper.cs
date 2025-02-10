using System.Security.Cryptography;

namespace Application.Helper
{
    public static class StringHelper
    {
        public static string GeneratorRandomStringBase64(int bytes)
        {
            var rand = new byte[bytes];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(rand);
            }
            return Convert.ToBase64String(rand);
        }
    }
}