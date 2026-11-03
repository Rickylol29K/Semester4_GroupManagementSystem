//register page

import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { homePage, register } from "../api.js";

export default function Register() {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [role, setRole] = useState("Student");
  const [error, setError] = useState("");

  //backend logs you in straight away
  async function handleSubmit(event) {
    event.preventDefault();
    try {
      const user = await register(email, password, role);
      navigate(homePage(user));
    } catch (err) {
      setError(err.message);
    }
  }

  return (
    <main className="login-page">
      <form className="login-panel" onSubmit={handleSubmit}>
        <h1>Create account</h1>

        <label htmlFor="email">Email</label>
        <input type="email" id="email" required value={email} onChange={(e) => setEmail(e.target.value)} />

        <label htmlFor="password">Password (at least 8 characters)</label>
        <input type="password" id="password" required minLength={8} value={password} onChange={(e) => setPassword(e.target.value)} />

        <label htmlFor="role">I am a</label>
        <select id="role" value={role} onChange={(e) => setRole(e.target.value)}>
          <option value="Student">Student</option>
          <option value="Teacher">Teacher</option>
        </select>

        {error && <p className="error">{error}</p>}

        <div className="login-options">
          <Link to="/">Back to log in</Link>
          <button type="submit">Register</button>
        </div>
      </form>
    </main>
  );
}
