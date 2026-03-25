import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

const backendTarget =
    process.env.BACKEND_HTTPS ??
    process.env.BACKEND_HTTP ??
    "http://localhost:5552";

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      // Proxy API calls to the app service
      "/api": {
        target: backendTarget,
        changeOrigin: true,
      },
    },
  },
});
