using System.Security.Cryptography;

namespace WorldTracker.Common
{
    public static class PasswordUtils
    {
        const string DELIMITER = ";";
        const int SALT_SIZE = 16;
        const int NUMBER_OF_ITERATIONS = 10000;
        const int HASH_SIZE = 32;

        public static string GenerateHash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SALT_SIZE);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, NUMBER_OF_ITERATIONS, HashAlgorithmName.SHA256, HASH_SIZE);

            return string.Join(DELIMITER, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public static bool ValidateHash(string inputPassword, string storedPassword)
        {
            try
            {
                var parts = storedPassword.Split(DELIMITER);
                var salt = Convert.FromBase64String(parts.First());
                var storedHash = Convert.FromBase64String(parts.Last());

                var hash = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, NUMBER_OF_ITERATIONS, HashAlgorithmName.SHA256, HASH_SIZE);

                return CryptographicOperations.FixedTimeEquals(storedHash, hash);
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
