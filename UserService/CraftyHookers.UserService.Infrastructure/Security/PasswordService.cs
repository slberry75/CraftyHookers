using CraftyHookers.UserService.Core.Security;
using System.Security.Cryptography;
using System.Text;

namespace CraftyHookers.UserService.Infrastructure.Security
{
    public class PasswordService : IPasswordHasher, IPasswordGenerator
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const byte FormatMarker = 0x01;

        // Kept high per OWASP password-storage guidance so brute-forcing a stolen hash is impractical.
        private const int Iterations = 210_000;
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

        private const string GenerationCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";

        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, Iterations, Algorithm, HashSize);

            var result = new byte[1 + sizeof(int) + SaltSize + HashSize];
            result[0] = FormatMarker;
            BitConverter.GetBytes(Iterations).CopyTo(result, 1);
            salt.CopyTo(result, 1 + sizeof(int));
            hash.CopyTo(result, 1 + sizeof(int) + SaltSize);

            return Convert.ToBase64String(result);
        }

        public bool Verify(string password, string hash)
        {
            byte[] decoded;
            try
            {
                decoded = Convert.FromBase64String(hash);
            }
            catch (FormatException)
            {
                return false;
            }

            if (decoded.Length != 1 + sizeof(int) + SaltSize + HashSize || decoded[0] != FormatMarker)
            {
                return false;
            }

            var iterations = BitConverter.ToInt32(decoded, 1);
            var salt = decoded[(1 + sizeof(int))..(1 + sizeof(int) + SaltSize)];
            var expectedHash = decoded[(1 + sizeof(int) + SaltSize)..];

            var actualHash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, iterations, Algorithm, HashSize);

            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }

        public string Generate(int length = 12)
        {
            return string.Create(length, GenerationCharacters, static (span, chars) =>
            {
                for (var i = 0; i < span.Length; i++)
                {
                    span[i] = chars[RandomNumberGenerator.GetInt32(chars.Length)];
                }
            });
        }
    }
}
