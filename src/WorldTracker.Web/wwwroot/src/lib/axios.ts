import axios from "axios";

declare global {
  interface Window {
    env: {
      VITE_BASE_URL: string;
    };
  }
}

const API = axios.create({
  baseURL: window.env?.VITE_BASE_URL,
});

if (localStorage.getItem("Token")) {
  const bearerToken = `Bearer ${localStorage.getItem("Token")}`;

  API.defaults.headers.common["Authorization"] = bearerToken;
}

export default API;