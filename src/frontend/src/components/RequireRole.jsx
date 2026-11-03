// Guards a page: only shows it if a user with the right role is logged in,
// otherwise sends them to the login page or to their own start page.

import { useEffect, useState } from "react";
import { Navigate } from "react-router-dom";
import { getCurrentUser, homePage } from "../api.js";

export default function RequireRole({ role, children }) {
  const [user, setUser] = useState(undefined);

  useEffect(() => {
    getCurrentUser().then(setUser);
  }, []);

  // Still checking
  if (user === undefined) return null;

  // Not logged in
  if (user === null) return <Navigate to="/" />;

  // Logged in, but this page is for the other role
  if (user.role !== role) return <Navigate to={homePage(user)} />;

  return children;
}
