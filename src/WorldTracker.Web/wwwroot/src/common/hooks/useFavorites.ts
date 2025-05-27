import useAppContext from "../context/AppContext";

export function useFavorites() {
  const { favorites, setFavorites } = useAppContext();

  function toggleFavorite(id: string) {
    setFavorites((currentFavorites) => {
      const exists = currentFavorites.includes(id);

      if (exists) {
        return currentFavorites.filter((fav) => fav !== id);
      } else {
        return [...currentFavorites, id];
      }
    });
  }

  return { favorites, setFavorites, toggleFavorite };
}
