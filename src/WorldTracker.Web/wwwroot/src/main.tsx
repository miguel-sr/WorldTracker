import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";

import { AppProvider } from "./common/context/AppContext";

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <React.StrictMode>
    <AppProvider>
      <App />
    </AppProvider>
  </React.StrictMode>
);
