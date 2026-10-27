import { useEffect, useState } from "react";
import { Navigate } from "react-router-dom";
import { getCurrentUser, homePage } from "../api.js";

//only shows the page when someone with the right role is logged in.
export default function RequireRole({ role, children }) {
  const [user, setUser] = useState(undefined);

  useEffect(() => {
    getCurrentUser().then(setUser);
  }, []);

  //still checking
  if (user === undefined) return null;

  //not logged in
  if (user === null) return <Navigate to="/" />;

  //logged in, but this page is for the other role
  if (user.role !== role) return <Navigate to={homePage(user)} />;

  return children;
}
