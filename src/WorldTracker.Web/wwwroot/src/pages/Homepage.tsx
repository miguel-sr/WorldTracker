import Footer from "@/components/Footer";
import Navbar from "@/components/Navbar";
import Banner from "./components/Banner";
import CountriesWithWeather from "./components/CountriesWithWeather";

export default function Homepage() {
  return (
    <>
      <Navbar position="relative" />
      <main>
        <Banner />
        <section className="mb-10 mx-auto p-6 bg-sky-blue">
          <h2 className="text-3xl font-semibold mb-4 text-center text-white">
            Busque por seu país
          </h2>
          <form
            className="max-w-md mx-auto flex items-center border border-gray-300 rounded-lg bg-white shadow-sm focus-within:ring-2 focus-within:ring-sky-500 px-4 py-2 transition-shadow"
            role="search"
            aria-label="Busca de cidade para clima"
          >
            <input
              type="text"
              name="city"
              placeholder="Digite o nome do país"
              className="flex-grow text-gray-700 placeholder-gray-400 bg-transparent focus:outline-none"
              required
            />
            <button
              type="submit"
              className="ml-3 bg-sky-600 hover:bg-sky-700 text-white font-medium rounded-lg px-4 py-2 transition-colors"
              aria-label="Buscar clima"
            >
              Buscar
            </button>
          </form>
        </section>
        {/* {weather && <WeatherCard weather={weather} />} */}
        <CountriesWithWeather />
        <div className="p-6 bg-gray-50 flex flex-wrap gap-6 justify-center">
          {[
            { code: "01d", desc: "clear sky (day)" },
            { code: "01n", desc: "clear sky (night)" },
            { code: "02d", desc: "few clouds (day)" },
            { code: "02n", desc: "few clouds (night)" },
            { code: "03d", desc: "scattered clouds (day)" },
            { code: "03n", desc: "scattered clouds (night)" },
            { code: "04d", desc: "broken clouds (day)" },
            { code: "04n", desc: "broken clouds (night)" },
            { code: "09d", desc: "shower rain (day)" },
            { code: "09n", desc: "shower rain (night)" },
            { code: "10d", desc: "rain (day)" },
            { code: "10n", desc: "rain (night)" },
            { code: "11d", desc: "thunderstorm (day)" },
            { code: "11n", desc: "thunderstorm (night)" },
            { code: "13d", desc: "snow (day)" },
            { code: "13n", desc: "snow (night)" },
            { code: "50d", desc: "mist (day)" },
            { code: "50n", desc: "mist (night)" },
          ].map(({ code, desc }) => (
            <div
              key={code}
              className="flex flex-col items-center justify-center w-24 p-3 rounded-md bg-blue-300 border-2"
            >
              <img
                src={`https://openweathermap.org/img/wn/${code}@4x.png`}
                alt={desc}
                className="w-16 h-16 mb-1"
              />
              <span className="text-white text-xs text-center">{desc}</span>
            </div>
          ))}
        </div>
      </main>
      <Footer />
    </>
  );
}
