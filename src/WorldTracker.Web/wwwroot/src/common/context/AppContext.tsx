import { ReactNode, createContext, useContext, useState } from "react";

const AppContext = createContext({
  loadingState: false,
  setLoadingState: (_state: boolean) => {},
  isUserAuthenticated: false,
  setIsUserAuthenticated: (_state: boolean) => {},
});

export function AppProvider({ children }: { children: ReactNode }) {
  const [loadingState, setLoadingState] = useState<boolean>(false);
  const [isUserAuthenticated, setIsUserAuthenticated] = useState<boolean>(false);

  return (
    <AppContext.Provider
      value={{
        loadingState,
        setLoadingState,
        isUserAuthenticated,
        setIsUserAuthenticated,
      }}
    >
      {children}
    </AppContext.Provider>
  );
}

export default function useAppContext() {
  return useContext(AppContext);
}
