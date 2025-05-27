import jwtDecode from "jwt-decode";

interface JwtPayload {
  sub: string;
}

export function getUserIdFromToken(): string | null {
  const token = localStorage.getItem("token");
  if (!token) return null;

  try {
    const decoded = jwtDecode<JwtPayload>(token);

    return decoded.sub;
  } catch {
    return null;
  }
}
