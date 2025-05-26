using Amazon.DynamoDBv2.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WorldTracker.Common.Helpers
{
    public static class PaginationTokenHelper
    {
        public static string? Encode(Dictionary<string, AttributeValue>? key)
        {
            if (key == null || key.Count == 0)
                return null;

            var token = JsonSerializer.SerializeToUtf8Bytes(key, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            });

            return Convert.ToBase64String(token);
        }

        public static Dictionary<string, AttributeValue>? Decode(string? token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            try
            {
                var bytes = Convert.FromBase64String(token);
                return JsonSerializer.Deserialize<Dictionary<string, AttributeValue>>(bytes);
            }
            catch (FormatException)
            {
                return null;
            }
            catch (JsonException)
            {
                return null;
            }
        }
    }
}
