//login page. Sends email + password to the backend and goes to the right start page.

import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { homePage, login } from "../api.js";

export default function Login() {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [staySignedIn, setStaySignedIn] = useState(false);
  const [error, setError] = useState("");

  //log in, then go to the student or teacher start page.
  async function handleSubmit(event) {
    event.preventDefault();
    try {
      const user = await login(email, password, staySignedIn);
      navigate(homePage(user));
    } catch (err) {
      setError(err.message);
    }
  }

  return (
    <main className="login-page">
      <form className="login-panel" onSubmit={handleSubmit}>
        <h1>Authentication</h1>

        <label htmlFor="email">Email</label>
        <input type="email" id="email" required value={email} onChange={(e) => setEmail(e.target.value)} />

        <label htmlFor="password">Password</label>
        <input type="password" id="password" required value={password} onChange={(e) => setPassword(e.target.value)} />

        {error && <p className="error">{error}</p>}

        <div className="login-options">
          <div>
            <label>
              <input type="checkbox" checked={staySignedIn} onChange={(e) => setStaySignedIn(e.target.checked)} /> Stay signed in
            </label><br />
            <Link to="/register">Create an account</Link>
          </div>
          <button type="submit">Log in</button>
        </div>
      </form>
    </main>
  );
}
