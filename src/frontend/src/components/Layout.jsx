//sidebar + page content

import { NavLink, useNavigate } from "react-router-dom";
import { logout } from "../api.js";

const studentLinks = [
  { to: "/student/group", label: "Group" },
  { to: "/student/dashboard", label: "Dashboard" },
];

const teacherLinks = [
  { to: "/teacher/groups", label: "Groups" },
];

//NavLink makes the current page bold
export default function Layout({ role, className = "", children }) {
  const navigate = useNavigate();
  const links = role === "Teacher" ? teacherLinks : studentLinks;

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
        {role === "Teacher" && <a href="#">Classes</a>}
        <div className="spacer"></div>
        {role === "Student" && <NavLink to="/student/profile">Personal Info</NavLink>}
        <a href="#" onClick={handleLogout}>Log out</a>
      </nav>

      <main className={"content " + className}>{children}</main>
    </div>
  );
}
