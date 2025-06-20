import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

import { useAuth } from "@/common/hooks/useAuth";
import { usePagination } from "@/common/hooks/usePagination";
import useRequestHandler from "@/common/hooks/useRequestHandler";

import WeatherRepository, {
  ICountryWithWeather,
} from "@/repository/CountryWithWeatherRepository";
import UserFavoritesRepository from "@/repository/UserFavoritesRepository";

import { useFavorites } from "@/common/hooks/useFavorites";
import Notistack from "@/lib/Notistack";
import CountryFilter from "./components/CountryFilter";
import CountryWithWeatherCard from "./components/CountryWithWeatherCard";
import CountryWithWeatherSkeleton from "./components/CountryWithWeatherSkeleton";

const PAGE_SIZE = 6;

export default function CountriesWithWeather() {
  const navigate = useNavigate();
  const { isAuthenticated, user } = useAuth();
  const { favorites } = useFavorites();
  const { showLoading } = useRequestHandler();

  const {
    items: countries,
    nextToken,
    previousTokens,
    loadPage,
    goNext,
    goPrev,
    filter,
    setFilter,
    loading,
  } = usePagination<ICountryWithWeather>((token, filter) =>
    WeatherRepository().GetPaged(PAGE_SIZE, token, filter)
  );

  useEffect(() => {
    loadPage(null);
  }, []);

  function syncFavorites() {
    if (!isAuthenticated || !user) {
      navigate("/login");
      return;
    }

    showLoading(async () => {
      await UserFavoritesRepository()
        .SyncFavoritesByUser(user.id, favorites)
        .then(() => {
          Notistack.showSuccessMessage("Favoritos sincronizados.");
        });
    });
  }

  return (
    <>
      <CountryFilter filter={filter} setFilter={setFilter} />
      <section className="my-10 mx-3">
        <div className="grid gap-6 justify-center grid-cols-1 sm:grid-cols-2 md:grid-cols-3">
          {loading
            ? Array.from({ length: PAGE_SIZE }).map((_, i) => (
                <CountryWithWeatherSkeleton key={i} />
              ))
            : countries.map((c) => (
                <CountryWithWeatherCard key={c.name} country={c} />
              ))}
        </div>

        <div className="flex flex-col sm:flex-row justify-center items-center gap-4 mt-8">
          <button
            onClick={goPrev}
            disabled={previousTokens.length === 0}
            className="w-full sm:w-auto px-4 py-2 rounded bg-primary-blue text-white disabled:opacity-50"
          >
            Anterior
          </button>

          {isAuthenticated && (
            <button
              onClick={syncFavorites}
              className="w-full sm:w-auto px-4 py-2 rounded bg-sky-600 text-white disabled:opacity-50"
            >
              Sincronizar Favoritos
            </button>
          )}

          <button
            onClick={goNext}
            disabled={!nextToken}
            className="w-full sm:w-auto px-4 py-2 rounded bg-primary-blue text-white disabled:opacity-50"
          >
            Próxima
          </button>
        </div>
      </section>
    </>
  );
}
