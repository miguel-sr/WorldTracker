import "swiper/css";
import "swiper/css/navigation";
import "./style.css";

import { useAuth } from "@/common/hooks/useAuth";
import { useFavorites } from "@/common/hooks/useFavorites";
import { useFavoriteCountries } from "@/common/hooks/useFavoritesCountries";
import useRequestHandler from "@/common/hooks/useRequestHandler";
import UserFavoritesRepository from "@/repository/UserFavoritesRepository";
import { useEffect } from "react";
import { Navigation } from "swiper/modules";
import { Swiper, SwiperSlide } from "swiper/react";
import CountryWithWeatherCard from "../CountriesWithWeather/components/CountryWithWeatherCard";

export default function Favorites() {
  const { setFavorites } = useFavorites();
  const { user } = useAuth();
  const { showLoading } = useRequestHandler();

  useEffect(() => {
    if (!user) return;

    showLoading(async () => {
      const favorites = await UserFavoritesRepository().GetAllByUser(user.id);
      setFavorites(favorites);
    });
  }, [user]);

  const countries = useFavoriteCountries();

  if (countries.length === 0) return null;

  return (
    <section className="my-10 mx-3">
      <h2 className="text-2xl font-semibold text-center text-dark-gray">
        Seus Favoritos
      </h2>
      <Swiper
        modules={[Navigation]}
        spaceBetween={50}
        slidesPerView={1}
        className="swiper-favorites md:px-14 py-5"
        navigation
        simulateTouch
        breakpoints={{
          640: {
            slidesPerView: 1,
            spaceBetween: 20,
          },
          768: {
            slidesPerView: 2,
            spaceBetween: 30,
          },
          1024: {
            slidesPerView: 3,
            spaceBetween: 40,
          },
        }}
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
