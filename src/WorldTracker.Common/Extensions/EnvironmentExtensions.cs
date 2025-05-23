namespace WorldTracker.Common.Extensions
{
    public static class EnvironmentExtensions
    {
        public static string GetRequiredEnvironmentVariable(this string variableName)
        {
            var value = Environment.GetEnvironmentVariable(variableName);

            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidOperationException( $"Environment variable '{variableName}' is not set or is empty.");

            return value;
        }
    }
}
