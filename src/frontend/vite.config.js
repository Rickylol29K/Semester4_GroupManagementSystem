//vite settings

import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

export default defineConfig({
  plugins: [react()],
  server: {
    //send /api calls to the backend
    proxy: {
      "/api": "http://localhost:5100",
    },
  },
});
