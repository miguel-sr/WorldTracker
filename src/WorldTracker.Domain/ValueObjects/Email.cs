using System.Text.RegularExpressions;

namespace WorldTracker.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be empty.");

            if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase))
                throw new ArgumentException("Invalid email format.");

            Value = value.Trim();
        }

        public static bool operator ==(Email? left, Email? right) => Equals(left, right);

        public static bool operator !=(Email? left, Email? right) => !Equals(left, right);

        public override bool Equals(object? obj) => obj is Email other && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;

        public static implicit operator string(Email email) => email.Value;

        public static explicit operator Email(string value) => new(value);
    }
}
