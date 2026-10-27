//the sidebar menu + page content, shared by every page after logging in.

import { NavLink, useNavigate } from "react-router-dom";
import { logout } from "../api.js";

const studentLinks = [
  { to: "/student/group", label: "Group" },
  { to: "/student/dashboard", label: "Dashboard" },
];

const teacherLinks = [
  { to: "/teacher/groups", label: "Groups" },
];

//sidebar + page content. NavLink adds the "active" class for the current page.
export default function Layout({ role, className = "", children }) {
  const navigate = useNavigate();
  const links = role === "teacher" ? teacherLinks : studentLinks;

  async function handleLogout() {
    await logout();
    navigate("/");
  }

  return (
    <div className="app">
      <nav className="sidebar">
        {links.map((link) => (
          <NavLink key={link.to} to={link.to}>{link.label}</NavLink>
        ))}
        {role === "teacher" && <a href="#">Classes</a>}
        <div className="spacer"></div>
        {role === "student" && <NavLink to="/student/profile">Personal Info</NavLink>}
        <a href="#" onClick={handleLogout}>Log out</a>
      </nav>

      <main className={"content " + className}>{children}</main>
    </div>
  );
}
