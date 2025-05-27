import { useAuth } from "@/common/hooks/useAuth";
import Footer from "@/components/Footer";
import Navbar from "@/components/Navbar";
import Banner from "./components/Banner";
import CountriesWithWeather from "./components/CountriesWithWeather";
import Favorites from "./components/Favorites";

export default function Homepage() {
  const { isAuthenticated } = useAuth();

  return (
    <>
      <Navbar position="relative" />
      <main>
        <Banner />
        {isAuthenticated && <Favorites />}
        <CountriesWithWeather />
      </main>
      <Footer />
    </>
  );
}
