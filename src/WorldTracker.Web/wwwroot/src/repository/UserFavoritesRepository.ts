import API from "@/lib/axios";

export default function UserFavoritesRepository() {
  async function GetAllByUser(userId: string): Promise<string[]> {
    const response = await API.get(`userFavorite/${userId}`);

    return response.data;
  }

  async function SyncFavoritesByUser(
    userId: string,
    favoriteIds: string[]
  ): Promise<void> {
    await API.put("userFavorite", {
      userId,
      favoriteIds,
    });
  }

  return {
    GetAllByUser,
    SyncFavoritesByUser,
  };
}
