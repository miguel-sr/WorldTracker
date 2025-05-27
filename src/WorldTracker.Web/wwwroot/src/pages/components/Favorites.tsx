import "swiper/css";
import "swiper/css/navigation";

import { useAuth } from "@/common/hooks/useAuth";
import { useFavorites } from "@/common/hooks/useFavorites";
import { useFavoriteCountries } from "@/common/hooks/useFavoritesCountries";
import useRequestHandler from "@/common/hooks/useRequestHandler";
import UserFavoritesRepository from "@/repository/UserFavoritesRepository";
import { useEffect } from "react";
import { Swiper, SwiperSlide } from "swiper/react";
import CountryWithWeatherCard from "./CountriesWithWeather/components/CountryWithWeatherCard";

export default function Favorites() {
  const { setFavorites } = useFavorites();
  const { userId } = useAuth();
  const { showLoading } = useRequestHandler();

  useEffect(() => {
    if (!userId) return;

    showLoading(async () => {
      const favorites = await UserFavoritesRepository().GetAllByUser(userId);
      setFavorites(favorites);
    });
  }, [userId]);

  const countries = useFavoriteCountries();

  if (countries.length === 0) return null;

  return (
    <section className="my-10 mx-3">
      <h2 className="text-2xl font-semibold text-center mb-6">
        Seus Favoritos
      </h2>
      <Swiper
        spaceBetween={50}
        slidesPerView={3}
        className="px-14 py-5"
        centerInsufficientSlides
        navigation
      >
        {countries.map((c) => (
          <SwiperSlide key={c.name}>
            <CountryWithWeatherCard country={c} />
          </SwiperSlide>
        ))}
      </Swiper>
    </section>
  );
}
