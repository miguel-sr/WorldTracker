import { ICountryWithWeather } from "@/repository/CountryRepository";
import { CountryDetails } from "./CountryDetails";
import { CountryHeader } from "./CountryHeader";
import { CountryWeatherInfo } from "./CountryWeatherInfo";

export default function CountryWithWeatherCard({
  country,
}: {
  country: ICountryWithWeather;
}) {
  return (
    <section className="w-full max-w-[600px] rounded-xl shadow-md p-6 mx-auto bg-gray-50 flex flex-col gap-6">
      <CountryHeader
        name={country.name}
        code={country.code}
        flag={country.flag}
      />
      <div className="flex flex-col md:flex-row justify-between gap-6">
        <div className="w-1/2">
          <CountryWeatherInfo weather={country.weather} />
        </div>
        <div className="w-1/2 pl-5">
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
