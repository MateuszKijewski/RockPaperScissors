using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using RockPaperScissors.Application.Common.Interfaces;
using System.Security.Cryptography;

namespace RockPaperScissors.Application.Sercurity.Services
{
    public class HashingService : IHashingService
    {
        public byte[] GetSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            return salt;
        }

        public string Hash(string input, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(input, salt, KeyDerivationPrf.HMACSHA1, 1000, 256 / 8));
        }
    }
}