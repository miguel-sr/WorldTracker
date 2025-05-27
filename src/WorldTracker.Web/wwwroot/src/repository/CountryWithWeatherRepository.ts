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
  category: string;
  currency: ICurrency;
  languages: string[];
  coordinates: ICoordinates;
  weather: IWeather;
}

export interface IPaginatedCountriesWithWeather {
  paginationToken: string | null;
  items: ICountryWithWeather[];
}

export default function CountryWithWeatherRepository() {
  async function GetPaged(
    pageSize: number,
    paginationToken: string | null,
    filter?: string
  ): Promise<IPaginatedCountriesWithWeather> {
    const response = await API.get("/countryWithWeather/paged", {
      params: { pageSize, paginationToken, filter },
    });

    return response.data;
  }

  async function GetByCodes(codes: string[]): Promise<ICountryWithWeather[]> {
    const response = await API.get("/countryWithWeather/byCodes", {
      params: { codes: codes.join(",") },
    });

    return response.data;
  }

  return {
    GetPaged,
    GetByCodes,
  };
}
