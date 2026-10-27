import { useNavigate } from "react-router-dom";

export default function Login() {
  const navigate = useNavigate();

  //go to the student group page after logging in.
  function handleSubmit(event) {
    event.preventDefault();
    navigate("/student/group");
  }

  return (
    <main className="login-page">
      <form className="login-panel" onSubmit={handleSubmit}>
        <h1>Authentication</h1>

        <label htmlFor="email">Email</label>
        <input type="email" id="email" />

        <label htmlFor="password">Password</label>
        <input type="password" id="password" />

        <div className="login-options">
          <div>
            <label><input type="checkbox" /> Stay signed in</label><br />
            <a href="#">Forgot password?</a>
          </div>
          <button type="submit">Log in</button>
        </div>
      </form>
    </main>
  );
}
