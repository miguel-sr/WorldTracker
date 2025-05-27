import { ICountryWithWeather } from "@/repository/CountryWithWeatherRepository";

export function CountryDetails({
  population,
  currency,
  languages,
}: Pick<ICountryWithWeather, "population" | "currency" | "languages">) {
  const langsStr =
    languages && languages.length > 0 ? languages.join(", ") : "Não disponível";

  return (
    <div className="space-y-2 text-sm text-gray-700">
      <p>
        <span className="font-semibold">População:</span>{" "}
        {population?.toLocaleString("pt-BR")}
      </p>
      <p
        className="cursor-help"
        title={currency?.name ? currency.name : undefined}
      >
        <span className="font-semibold">Moeda:</span>{" "}
        {currency?.code && currency?.symbol
          ? `${currency.symbol} (${currency.code})`
          : "Não disponível"}
      </p>
      <p title={langsStr} className="line-clamp-2 cursor-help">
        <span className="font-semibold ">Idioma(s):</span> {langsStr}
      </p>
    </div>
  );
}
