import { TOKEN_KEY } from "@/common/Constants";
import jwtDecode from "jwt-decode";
import { useCallback, useEffect, useState } from "react";

interface JwtPayload {
  nameid: string;
  exp: number;
}

export function useAuth() {
  const [userId, setUserId] = useState<string | null>(null);
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  const loadToken = useCallback(() => {
    const token = localStorage.getItem(TOKEN_KEY);
    if (!token) {
      setUserId(null);
      setIsAuthenticated(false);
      return;
    }
    try {
      const decoded = jwtDecode<JwtPayload>(token);
      const currentTime = Date.now() / 1000;

      if (decoded.exp < currentTime) {
        localStorage.removeItem(TOKEN_KEY);
        setUserId(null);
        setIsAuthenticated(false);
      } else {
        setUserId(decoded.nameid);
        setIsAuthenticated(true);
      }
    } catch {
      localStorage.removeItem(TOKEN_KEY);
      setUserId(null);
      setIsAuthenticated(false);
    }
  }, []);

  useEffect(() => {
    loadToken();
  }, [loadToken]);

  function logout() {
    localStorage.removeItem(TOKEN_KEY);
    setUserId(null);
    setIsAuthenticated(false);
    location.replace("/");
  }

  return {
    userId,
    isAuthenticated,
    loadToken,
    logout,
  };
}
