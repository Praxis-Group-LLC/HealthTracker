// src/main.tsx
import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import { AppRouter } from "./router/AppRouter";
import { ThemeProvider } from "./theme/ThemeContext";
import "./index.css";
import { DeviceAuthProvider } from "./auth/DeviceAuthContext";

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <React.StrictMode>
    <ThemeProvider>
      <DeviceAuthProvider>
        <BrowserRouter>
          <AppRouter />
        </BrowserRouter>
      </DeviceAuthProvider>
    </ThemeProvider>
  </React.StrictMode>
);
