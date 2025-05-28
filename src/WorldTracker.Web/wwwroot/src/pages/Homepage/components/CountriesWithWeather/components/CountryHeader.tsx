import { useAuth } from "@/common/hooks/useAuth";
import { useFavorites } from "@/common/hooks/useFavorites";
import { LuStar } from "react-icons/lu";
import { yellow, zinc, white } from "tailwindcss/colors";

export function CountryHeader({
  name,
  code,
  flag,
}: {
  name: string;
  code: string;
  flag: { url: string; altText: string };
}) {
  const favoriteId = `country#${code}`;
  const { favorites, toggleFavorite } = useFavorites();
  const { isAuthenticated } = useAuth();

  const favorited = favorites.includes(favoriteId);

  return (
    <div className="flex items-center justify-between">
      <div className="flex items-center gap-x-3 max-w-[80%] md:max-w-[90%]">
        {isAuthenticated && (
          <button
            onClick={() => toggleFavorite(favoriteId)}
            aria-label={
              favorited ? "Remover dos favoritos" : "Adicionar aos favoritos"
            }
            className="top-4 right-4 focus:outline-none"
          >
            <LuStar
              size={30}
              fill={favorited ? yellow[400] : white}
              color={favorited ? yellow[400] : zinc[400]}
            />
          </button>
        )}
        <h3
          className="text-2xl font-semibold truncate cursor-help"
          title={`${name}, ${code}`}
        >
          {name}, {code}
        </h3>
      </div>

      <img
        src={flag.url}
        alt={flag.altText || `Bandeira de ${name}`}
        className="h-[35px] object-cover rounded-sm border border-gray-300"
      />
    </div>
  );
}
