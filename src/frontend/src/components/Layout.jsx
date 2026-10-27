import { NavLink } from "react-router-dom";

const studentLinks = [
  { to: "/student/group", label: "Group" },
  { to: "/student/dashboard", label: "Dashboard" },
];

const teacherLinks = [
  { to: "/teacher/groups", label: "Groups" },
];

//sidebar + page content. NavLink adds the "active" class for the current page.
export default function Layout({ role, className = "", children }) {
  const links = role === "teacher" ? teacherLinks : studentLinks;

  return (
    <div className="app">
      <nav className="sidebar">
        {links.map((link) => (
          <NavLink key={link.to} to={link.to}>{link.label}</NavLink>
        ))}
        {role === "teacher" && <a href="#">Classes</a>}
        <div className="spacer"></div>
        {role === "student" && <NavLink to="/student/profile">Personal Info</NavLink>}
        <NavLink to="/" end>Log out</NavLink>
      </nav>

      <main className={"content " + className}>{children}</main>
    </div>
  );
}
