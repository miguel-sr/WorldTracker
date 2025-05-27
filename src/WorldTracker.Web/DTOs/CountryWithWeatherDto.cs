using WorldTracker.Domain.Entities;
using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Web.DTOs
{
    public class CountryWithWeatherDto
    {
        public required string Name { get; set; }

        public required string Code { get; set; }

        public required Flag Flag { get; set; }

        public required int Population { get; set; }

        public required string Category { get; set; }

        public CurrencyInfo? Currency { get; set; }

        public required string[] Languages { get; set; }

        public required Coordinates Coordinates { get; set; }

        public required Weather Weather { get; set; }

        public static CountryWithWeatherDto Parse(Country country, Weather weather)
        {
            return new CountryWithWeatherDto
            {
                Name = country.Name,
                Code = country.Code,
                Flag = country.Flag,
                Population = country.Population,
                Category = country.Category,
                Languages = country.Languages?.Select(x => $"{x.Value} ({x.Key.ToUpper()})").ToArray() ?? [],
                Currency = country.CurrencyInfo,
                Coordinates = country.Coordinates,
                Weather = weather
            };
        }
    }
}
