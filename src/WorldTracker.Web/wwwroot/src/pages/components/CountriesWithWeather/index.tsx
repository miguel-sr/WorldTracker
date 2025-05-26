import { usePagination } from "@/common/hooks/usePagination";
import useRequestHandler from "@/common/hooks/useRequestHandler";
import WeatherRepository, {
  ICountryWithWeather,
} from "@/repository/CountryRepository";
import { useEffect } from "react";
import CountryWithWeatherCard from "./components/CountryWithWeatherCard";

export default function CountriesWithWeather() {
  const PAGE_SIZE = 6;
  const fetchPage = (token: string | null) =>
    WeatherRepository().GetCountriesWithWeather(PAGE_SIZE, token);

  const {
    items: countries,
    currentToken,
    nextToken,
    previousTokens,
    loadPage,
    goNext,
    goPrev,
  } = usePagination<ICountryWithWeather>(fetchPage);

  const { showLoading } = useRequestHandler();

  useEffect(() => {
    showLoading(() => loadPage(currentToken));
  }, [currentToken]);

  return (
    <section className="my-10 mx-3">
      <div className="flex flex-wrap justify-center gap-y-10 gap-x-6">
        {countries.map((c) => (
          <CountryWithWeatherCard key={c.name} country={c} />
        ))}
      </div>

      <div className="flex justify-center gap-4 mt-8">
        <button
          onClick={goPrev}
          disabled={previousTokens.length === 0}
          className="px-4 py-2 rounded bg-sky-blue text-white disabled:opacity-50"
        >
          Anterior
        </button>

        <button
          onClick={goNext}
          disabled={!nextToken}
          className="px-4 py-2 rounded bg-sky-blue text-white disabled:opacity-50"
        >
          Próxima
        </button>
      </div>
    </section>
  );
}
