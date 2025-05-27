namespace WorldTracker.Domain.ValueObjects
{
    public class FavoriteId
    {
        public string Type { get; }
        public string Value { get; }

        public FavoriteId(string type, string value)
        {
            if (string.IsNullOrWhiteSpace(type)) 
                throw new ArgumentException("Type cannot be empty.");

            if (string.IsNullOrWhiteSpace(value)) 
                throw new ArgumentException("Value cannot be empty.");

            Type = type.ToLowerInvariant();
            Value = value;
        }

        public override string ToString() => $"{Type}#{Value}";

        public override bool Equals(object? obj) => obj is FavoriteId other && Type == other.Type && Value == other.Value;

        public override int GetHashCode() => HashCode.Combine(Type, Value);

        public static FavoriteId Parse(string raw)
        {
            var parts = raw.Split('#');

            if (parts.Length != 2)
                throw new ArgumentException("Invalid FavoriteId format. Expected format: 'type#value'.");

            return new FavoriteId(parts[0], parts[1]);
        }

        public static implicit operator string(FavoriteId favoriteId) => favoriteId.ToString();

        public static explicit operator FavoriteId(string value) => Parse(value);
    }
}
