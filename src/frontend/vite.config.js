import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

export default defineConfig({
  plugins: [react()],
  server: {
    //send /api calls to the .NET backend, so the login cookie works on the same address.
    proxy: {
      "/api": "http://localhost:5100",
    },
  },
});
