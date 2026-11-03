//only shows the page if the right role is logged in

import { useEffect, useState } from "react";
import { Navigate } from "react-router-dom";
import { getCurrentUser, homePage } from "../api.js";

export default function RequireRole({ role, children }) {
  const [user, setUser] = useState(undefined);

  useEffect(() => {
    getCurrentUser().then(setUser);
  }, []);

  //still checking
  if (user === undefined) return null;

  //not logged in
  if (user === null) return <Navigate to="/" />;

  //wrong role
  if (user.role !== role) return <Navigate to={homePage(user)} />;

  return children;
}
