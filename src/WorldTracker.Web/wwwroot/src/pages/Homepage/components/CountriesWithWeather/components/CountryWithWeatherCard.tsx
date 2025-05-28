import { ICountryWithWeather } from "@/repository/CountryWithWeatherRepository";
import { CountryDetails } from "./CountryDetails";
import { CountryHeader } from "./CountryHeader";
import { CountryWeatherInfo } from "./CountryWeatherInfo";

export default function CountryWithWeatherCard({
  country,
}: {
  country: ICountryWithWeather;
}) {
  return (
    <section className="w-full max-w-[600px] rounded-2xl shadow-md p-4 sm:p-6 bg-gray-50 flex flex-col gap-4 sm:gap-6 relative transition-all">
      <CountryHeader
        name={country.name}
        code={country.code}
        flag={country.flag}
      />
      <div className="flex flex-col md:flex-row md:justify-between gap-4 sm:gap-6">
        <div className="md:w-1/2">
          <CountryWeatherInfo weather={country.weather} />
        </div>
        <div className="md:w-1/2 md:pl-5">
          <CountryDetails
            population={country.population}
            currency={country.currency}
            languages={country.languages}
          />
        </div>
      </div>
    </section>
  );
}
