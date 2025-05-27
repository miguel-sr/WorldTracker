using Amazon.DynamoDBv2.DataModel;
using System.Text.Json.Serialization;
using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Domain.Entities
{
    [DynamoDBTable(TABLE_NAME)]

    public class Country
    {
        public const string TABLE_NAME = "Countries";

        [DynamoDBHashKey]
        public required string Code { get; set; }

        [DynamoDBProperty]
        public required string Name { get; set; }

        [DynamoDBProperty]
        [JsonIgnore]
        public string NameLower { get; set; }

        [DynamoDBProperty]
        public string Category { get; set; } = "Country";

        [DynamoDBProperty]
        public CurrencyInfo? CurrencyInfo { get; set; }

        [DynamoDBProperty]
        public required string Region { get; set; }

        [DynamoDBProperty]
        public required string Subregion { get; set; }

        [DynamoDBProperty]
        public int Population { get; set; }

        [DynamoDBProperty]
        public required Flag Flag { get; set; }

        [DynamoDBProperty]
        public Dictionary<string, string> Languages { get; set; } = [];

        [DynamoDBProperty]
        [JsonIgnore]
        public string CoordinatesRaw { get; set; }

        [DynamoDBIgnore]
        public Coordinates Coordinates
        {
            get => Coordinates.Parse(CoordinatesRaw );
            set => CoordinatesRaw  = value.ToString();
        }
    }
    
    public class CurrencyInfo
    {
        public required string Name { get; set; }

        public required string Code { get; set; }

        public required string Symbol { get; set; }
    }

    public class Flag
    {
        [DynamoDBProperty]
        public required string Url { get; set; }

        [DynamoDBProperty]
        public string? AltText { get; set; }
    }
}
