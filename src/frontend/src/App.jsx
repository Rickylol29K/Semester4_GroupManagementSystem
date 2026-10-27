import { Navigate, Route, Routes } from "react-router-dom";
import Login from "./pages/Login.jsx";
import Group from "./pages/student/Group.jsx";
import Dashboard from "./pages/student/Dashboard.jsx";
import Profile from "./pages/student/Profile.jsx";
import Groups from "./pages/teacher/Groups.jsx";

export default function App() {
  return (
    <Routes>
      <Route path="/" element={<Login />} />
      <Route path="/student/group" element={<Group />} />
      <Route path="/student/dashboard" element={<Dashboard />} />
      <Route path="/student/profile" element={<Profile />} />
      <Route path="/teacher/groups" element={<Groups />} />
      <Route path="*" element={<Navigate to="/" />} />
    </Routes>
  );
}
