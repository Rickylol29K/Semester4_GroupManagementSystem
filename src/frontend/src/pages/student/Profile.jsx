import { useState } from "react";
import Layout from "../../components/Layout.jsx";

const skills = ["Back end", "Front end", "Database", "UI/UX design", "Testing", "DevOps"];

export default function Profile() {
  const [status, setStatus] = useState("");

  //show a saved message. Nothing is stored yet.
  function handleSubmit(event) {
    event.preventDefault();
    setStatus("Saved.");
  }

  return (
    <Layout role="student">
      <form className="card profile-card" onSubmit={handleSubmit}>
        <h2>Personal Information</h2>

        <input type="text" placeholder="Full name" />
        <input type="email" placeholder="Email" />

        <details className="skills">
          <summary>Skills</summary>
          {skills.map((skill) => (
            <label key={skill}><input type="checkbox" /> {skill}</label>
          ))}
        </details>

        <button type="submit">Save</button>
        <p>{status}</p>
      </form>
    </Layout>
  );
}
