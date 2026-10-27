import { useState } from "react";
import Layout from "../../components/Layout.jsx";

const members = ["Student 1", "Student 2", "Student 3", "Student 4", "Student 5", "Student 6"];

export default function Group() {
  const [status, setStatus] = useState("");

  return (
    <Layout role="student" className="group-page">
      <div className="row">
        <ul className="list">
          {members.map((name) => <li key={name}>{name}</li>)}
        </ul>

        <div className="card">
          <h2>Justification</h2>
          <textarea placeholder="Why were these students grouped together?" readOnly></textarea>
        </div>
      </div>

      {/* accept the group or ask teacher for change */}
      <div className="actions">
        <button onClick={() => setStatus("You accepted this group.")}>Accept</button>
        <button onClick={() => setStatus("Your change request was sent to the teacher.")}>Request change</button>
      </div>
      <p>{status}</p>
    </Layout>
  );
}
