import { TOKEN_KEY } from "@/common/Constants";
import { AppProvider } from "@/common/context/AppContext";
import { Navigate, Outlet } from "react-router-dom";

export function ProtectedRoute() {
  function isTokenExpired(): boolean {
    const token = localStorage.getItem(TOKEN_KEY);

    console.log(token)

    if (!token)
      return true;

    console.log(token)

    const payload = JSON.parse(atob(token.split('.')[1]));
    const currentTime = Date.now() / 1000;

    console.log(payload.exp)
    console.log(currentTime)

    return payload.exp < currentTime;
  }

  if (isTokenExpired()) {
    return <Navigate to="/login" />;
  }

  return (
    <AppProvider>
      <Outlet />
    </AppProvider>
  );
}
