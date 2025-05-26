import axios from "axios";

const API = axios.create({
  baseURL: import.meta.env.VITE_BASE_URL,
});

if (localStorage.getItem("Token")) {
  const bearerToken = `Bearer ${localStorage.getItem("Token")}`;

  API.defaults.headers.common["Authorization"] = bearerToken;
}

export default API;