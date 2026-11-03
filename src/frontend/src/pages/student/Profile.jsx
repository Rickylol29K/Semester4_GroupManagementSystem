//student profile page

import { useEffect, useState } from "react";
import Layout from "../../components/Layout.jsx";
import { getProfile, saveProfile } from "../../api.js";

const skills = ["Back end", "Front end", "Database", "UI/UX design", "Testing", "DevOps"];
const weekDays = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"];

//text fields with their label
const textFields = [
  { name: "interests", label: "Interests" },
  { name: "strengths", label: "Strengths" },
  { name: "weaknesses", label: "Weaknesses" },
  { name: "learningGoals", label: "Learning goals" },
  { name: "workingPreferences", label: "Working preferences" },
];

const emptyProfile = {
  fullName: "",
  skills: [],
  interests: "",
  strengths: "",
  weaknesses: "",
  learningGoals: "",
  workingPreferences: "",
  availability: [],
};

export default function Profile() {
  const [profile, setProfile] = useState(emptyProfile);
  const [status, setStatus] = useState("");
  const [error, setError] = useState("");

  //load the saved profile
  useEffect(() => {
    getProfile().then(setProfile).catch((err) => setError(err.message));
  }, []);

  function setField(name, value) {
    setProfile({ ...profile, [name]: value });
    setStatus("");
  }

  //checked = add, unchecked = remove
  function toggle(listName, item) {
    const list = profile[listName];
    setField(listName, list.includes(item) ? list.filter((x) => x !== item) : [...list, item]);
  }

  async function handleSubmit(event) {
    event.preventDefault();
    setError("");
    try {
      setProfile(await saveProfile(profile));
      setStatus("Saved.");
    } catch (err) {
      setError(err.message);
    }
  }

  return (
    <Layout role="Student">
      <form className="card profile-card" onSubmit={handleSubmit}>
        <h2>Personal Information</h2>

        <label htmlFor="fullName">Full name</label>
        <input type="text" id="fullName" maxLength={100}
          value={profile.fullName} onChange={(e) => setField("fullName", e.target.value)} />

        <details className="skills">
          <summary>Skills ({profile.skills.length} selected)</summary>
          {skills.map((skill) => (
            <label key={skill}>
              <input type="checkbox" checked={profile.skills.includes(skill)} onChange={() => toggle("skills", skill)} /> {skill}
            </label>
          ))}
        </details>

        {textFields.map((field) => (
          <div className="field" key={field.name}>
            <label htmlFor={field.name}>{field.label}</label>
            <textarea id={field.name} rows={2} maxLength={1000}
              value={profile[field.name]} onChange={(e) => setField(field.name, e.target.value)} />
          </div>
        ))}

        <fieldset className="availability">
          <legend>Availability</legend>
          {weekDays.map((day) => (
            <label key={day}>
              <input type="checkbox" checked={profile.availability.includes(day)} onChange={() => toggle("availability", day)} /> {day}
            </label>
          ))}
        </fieldset>

        {error && <p className="error">{error}</p>}
        <button type="submit">Save</button>
        <p>{status}</p>
      </form>
    </Layout>
  );
}
