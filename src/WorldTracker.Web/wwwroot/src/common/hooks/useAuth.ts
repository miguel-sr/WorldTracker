import { TOKEN_KEY } from "@/common/Constants";
import jwtDecode from "jwt-decode";
import { useCallback, useEffect, useState } from "react";

interface JwtPayload {
  nameid: string;
  email: string;
  unique_name: string;
  exp: number;
}

interface IUser {
  id: string;
  email: string;
  name: string;
}

export function useAuth() {
  const [user, setUser] = useState<IUser | null>(null);
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  const loadToken = useCallback(() => {
    const token = localStorage.getItem(TOKEN_KEY);
    if (!token) {
      setUser(null);
      setIsAuthenticated(false);
      return;
    }
    try {
      const decoded = jwtDecode<JwtPayload>(token);
      const currentTime = Date.now() / 1000;

      if (decoded.exp < currentTime) {
        localStorage.removeItem(TOKEN_KEY);
        setUser(null);
        setIsAuthenticated(false);
      } else {
        setUser({
          id: decoded.nameid,
          email: decoded.email,
          name: decoded.unique_name,
        });

        setIsAuthenticated(true);
      }
    } catch {
      localStorage.removeItem(TOKEN_KEY);
      setUser(null);
      setIsAuthenticated(false);
    }
  }, []);

  useEffect(() => {
    loadToken();
  }, [loadToken]);

  function logout() {
    localStorage.removeItem(TOKEN_KEY);
    setUser(null);
    setIsAuthenticated(false);
    location.replace("/");
  }

  return {
    user,
    isAuthenticated,
    loadToken,
    logout,
  };
}
