import API from "@/lib/axios";

export interface ICoordinates {
  latitude: number;
  longitude: number;
}

export interface IWeather {
  description: string;
  icon: string;
  temperature: number;
  feelsLike: number;
  humidity: number;
  windSpeed: number;
}

export interface IFlag {
  url: string;
  altText: string;
}

export interface ICurrency {
  name: string;
  code: string;
  symbol: string;
}

export interface ICountryWithWeather {
  name: string;
  code: string;
  flag: IFlag;
  population: number;
  currency: ICurrency;
  languages: string[];
  coordinates: ICoordinates;
  weather: IWeather;
}

export interface IPaginatedCountriesWithWeather {
  paginationToken: string | null;
  items: ICountryWithWeather[];
}

export default function WeatherRepository() {
  async function GetCountriesWithWeather(
    pageSize: number,
    paginationToken: string | null
  ): Promise<IPaginatedCountriesWithWeather> {
    const response = await API.get("/country/paged", {
      params: { pageSize, paginationToken },
    });

    return response.data;
  }

  return {
    GetCountriesWithWeather,
  };
}
