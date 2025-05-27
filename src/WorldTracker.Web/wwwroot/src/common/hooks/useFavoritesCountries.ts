import CountryWithWeatherRepository, {
  ICountryWithWeather,
} from "@/repository/CountryWithWeatherRepository";
import { useEffect, useState } from "react";
import { useFavorites } from "./useFavorites";
import useRequestHandler from "./useRequestHandler";

export function useFavoriteCountries() {
  const { favorites } = useFavorites();
  const { showLoading } = useRequestHandler();
  const [countries, setCountries] = useState<ICountryWithWeather[]>([]);

  useEffect(() => {
    if (favorites.length === 0) {
      setCountries([]);
      return;
    }

    showLoading(async () => {
      const countries = await CountryWithWeatherRepository().GetByCodes(
        favorites.map((fav) => fav.replace("country#", ""))
      );
      setCountries(countries);
    });
  }, [favorites]);

  return countries;
}
