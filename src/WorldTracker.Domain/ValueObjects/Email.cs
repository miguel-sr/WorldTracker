using System.Text.RegularExpressions;

namespace WorldTracker.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email não pode ser vazio.");

            if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase))
                throw new ArgumentException("Email em formato inválido.");

            Value = value.Trim();
        }

        public override string ToString() => Value;

        public static implicit operator string(Email email) => email.Value;

        public static explicit operator Email(string value) => new(value);
    }
}
