using System.Text.Json.Serialization;
using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Infra.DTOs
{
    public class CountryResponseDto
    {
        public required Name Name { get; set; }

        [JsonPropertyName("cca2")]
        public required string IsoAlpha2Code { get; set; }

        [JsonPropertyName("ccn3")]
        public string? IsoNumericCode { get; set; }

        [JsonPropertyName("cca3")]
        public required string IsoAlpha3Code { get; set; }

        [JsonPropertyName("cioc")]
        public string? OlympicCode { get; set; }

        public int Population { get; set; }

        public required string Region { get; set; }

        public string? Subregion { get; set; }

        public Dictionary<string, Currency> Currencies { get; set; } = [];

        [JsonIgnore]
        public PrimaryCurrencyInfo? PrimaryCurrencyInfo
        {
            get
            {
                if (Currencies is null || !Currencies.Any())
                    return null;

                var first = Currencies.First();

                return new PrimaryCurrencyInfo(first.Key, first.Value.Symbol, first.Value.Name);
            }
        }

        public required Flags Flags { get; set; }

        public Dictionary<string, string> Languages { get; set; } = [];

        public required double[] Latlng { get; set; }

        [JsonIgnore]
        public Coordinates Coordinates => Latlng;

    }

    public class Name
    {
        public required string Common { get; set; }

        public required string Official { get; set; }
    }

    public class Currency
    {
        public required string Symbol { get; set; }

        public required string Name { get; set; }
    }

    public class PrimaryCurrencyInfo(string code, string symbol, string name)
    {
        public string Code { get; } = code;

        public string Symbol { get; } = symbol;

        public string Name { get; } = name;
    }

    public class Flags
    {
        public required string Svg { get; set; }
        public string? Alt { get; set; }
    }
}
