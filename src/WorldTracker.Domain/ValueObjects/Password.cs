using System.Security.Cryptography;

namespace WorldTracker.Domain.ValueObjects
{
    public class Password
    {
        private const int MIN_LENGTH = 8;
        private const int SALT_SIZE = 16;
        private const int HASH_SIZE = 32;
        private const int ITERATIONS = 10000;
        private const string DELIMITER = ";";

        public string Hash { get; }

        /// <summary>
        /// Creates a Password instance by hashing a plain-text password.
        /// </summary>
        /// <param name="password">The plain-text password to hash.</param>
        public Password(string password)
        {
            ValidatePassword(password);

            Hash = GenerateHash(password);
        }

        /// <summary>
        /// Creates a Password instance from either a plain-text password or a hashed value.
        /// </summary>
        /// <param name="value">The plain-text password or already hashed value.</param>
        /// <param name="isHashed">
        /// Set to <c>true</c> if the value is already hashed; otherwise, <c>false</c> to hash the plain-text password.
        /// </param>
        public Password(string value, bool isHashed)
        {
            Hash = isHashed ? value : GenerateHash(value);
        }

        /// <summary>
        /// Checks if a given plain-text password matches the stored hashed password.
        /// </summary>
        /// <param name="value">The plain-text password to verify.</param>
        /// <returns>True if the password matches; otherwise, false.</returns>
        public bool Matches(string value) => VerifyPassword(value, Hash);

        public override string ToString() => Hash;

        private string GenerateHash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SALT_SIZE);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, ITERATIONS, HashAlgorithmName.SHA256, HASH_SIZE);

            return string.Join(DELIMITER, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            try
            {
                ValidatePassword(inputPassword);

                var parts = storedHash.Split(DELIMITER);

                if (parts.Length != 2) return false;

                var salt = Convert.FromBase64String(parts[0]);
                var originalHash = Convert.FromBase64String(parts[1]);

                var inputHash = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, ITERATIONS, HashAlgorithmName.SHA256, HASH_SIZE);

                return CryptographicOperations.FixedTimeEquals(originalHash, inputHash);
            }
            catch
            {
                return false;
            }
        }

        private void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty or whitespace.");

            if (password.Length < MIN_LENGTH)
                throw new ArgumentException($"Password must be at least {MIN_LENGTH} characters long.");
        }
    }
}
