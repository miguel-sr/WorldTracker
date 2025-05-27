import {
  Dispatch,
  ReactNode,
  SetStateAction,
  createContext,
  useContext,
  useState,
} from "react";

type AppContextType = {
  loadingState: boolean;
  setLoadingState: Dispatch<SetStateAction<boolean>>;
  isUserAuthenticated: boolean;
  setIsUserAuthenticated: Dispatch<SetStateAction<boolean>>;
  favorites: string[];
  setFavorites: Dispatch<SetStateAction<string[]>>;
};

const AppContext = createContext<AppContextType>({
  loadingState: false,
  setLoadingState: () => {},
  isUserAuthenticated: false,
  setIsUserAuthenticated: () => {},
  favorites: [],
  setFavorites: () => {},
});

export function AppProvider({ children }: { children: ReactNode }) {
  const [loadingState, setLoadingState] = useState(false);
  const [isUserAuthenticated, setIsUserAuthenticated] = useState(false);
  const [favorites, setFavorites] = useState<string[]>([]);

  return (
    <AppContext.Provider
      value={{
        loadingState,
        setLoadingState,
        isUserAuthenticated,
        setIsUserAuthenticated,
        favorites,
        setFavorites,
      }}
    >
      {children}
    </AppContext.Provider>
  );
}

export default function useAppContext() {
  return useContext(AppContext);
}
