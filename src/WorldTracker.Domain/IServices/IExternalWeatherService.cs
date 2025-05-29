using WorldTracker.Domain.Entities;
using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Domain.IServices
{
    public interface IExternalWeatherService
    {
        Task<Weather> GetWeather(Coordinates coordinates);
    }
}
