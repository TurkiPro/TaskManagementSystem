using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace TaskManagementSystem.Helpers
{
    public class PasswordHelper
    {
        public static byte[] GetSecureSalt()
        {
            // RandomNumberGenerator Class to generate a secure random number bytes

            return RandomNumberGenerator.GetBytes(32);
        }
        public static string HashUsingPbkdf2(string password, byte[] salt)
        {
            byte[] derivedKey = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, iterationCount: 100000, 32);

            return Convert.ToBase64String(derivedKey);
        }
    }
}
