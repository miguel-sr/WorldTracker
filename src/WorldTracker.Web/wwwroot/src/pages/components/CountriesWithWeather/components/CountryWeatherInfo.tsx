import { ICountryWithWeather } from "@/repository/CountryRepository";

export function CountryWeatherInfo({
  weather,
}: {
  weather: ICountryWithWeather["weather"];
}) {
  return (
    <div className="flex flex-col gap-3">
      <div className="flex justify-between items-center">
        <div
          className="capitalize flex items-center cursor-help"
          title={weather.description}
        >
          <span className="flex-shrink-0 flex items-center justify-center w-14 aspect-square mr-3 rounded-lg bg-blue-300">
            <img
              src={`https://openweathermap.org/img/wn/${weather.icon}@4x.png`}
              alt={weather.description}
              className="w-12 aspect-square"
            />
          </span>
          <span className="line-clamp-2 max-w-xs">{weather.description}</span>
        </div>
        <div className="text-4xl font-light ml-2">
          {Math.round(weather.temperature)}°C
        </div>
      </div>

      <div className="flex justify-between text-xs">
        <div className="flex flex-col items-center">
          <span className="font-semibold">Sensação</span>
          <span>{Math.round(weather.feelsLike)}°C</span>
        </div>
        <div className="flex flex-col items-center">
          <span className="font-semibold">Umidade</span>
          <span>{weather.humidity}%</span>
        </div>
        <div className="flex flex-col items-center">
          <span className="font-semibold">Vento</span>
          <span>{weather.windSpeed} m/s</span>
        </div>
      </div>
    </div>
  );
}
