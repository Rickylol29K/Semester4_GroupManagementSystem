//all pages and who can see them

import { Navigate, Route, Routes } from "react-router-dom";
import RequireRole from "./components/RequireRole.jsx";
import Login from "./pages/Login.jsx";
import Register from "./pages/Register.jsx";
import Group from "./pages/student/Group.jsx";
import Dashboard from "./pages/student/Dashboard.jsx";
import Profile from "./pages/student/Profile.jsx";
import Groups from "./pages/teacher/Groups.jsx";

export default function App() {
  return (
    <Routes>
      <Route path="/" element={<Login />} />
      <Route path="/register" element={<Register />} />
      <Route path="/student/group" element={<RequireRole role="Student"><Group /></RequireRole>} />
      <Route path="/student/dashboard" element={<RequireRole role="Student"><Dashboard /></RequireRole>} />
      <Route path="/student/profile" element={<RequireRole role="Student"><Profile /></RequireRole>} />
      <Route path="/teacher/groups" element={<RequireRole role="Teacher"><Groups /></RequireRole>} />
      <Route path="*" element={<Navigate to="/" />} />
    </Routes>
  );
}
