//teacher page to make a project and see groups. Shows the students that have an account.

import { useEffect, useState } from "react";
import { getStudents } from "../../api.js";
import Layout from "../../components/Layout.jsx";

export default function Groups() {
  const [students, setStudents] = useState([]);
  const [studentName, setStudentName] = useState("");
  const [projectName, setProjectName] = useState("");
  const [requests, setRequests] = useState(["Student 1", "Student 2", "Student 3"]);
  const groups = ["Group 1", "Group 2", "Group 3", "Group 4"];

  //start with the students that have an account.
  useEffect(() => {
    getStudents().then((list) => setStudents(list.map((student) => student.email)));
  }, []);

  //add a student to the list.
  function addStudent(event) {
    event.preventDefault();
    setStudents([...students, studentName]);
    setStudentName("");
  }

  function handleRequest(name) {
    //approving or denying a change request removes it from the list.
    setRequests(requests.filter((request) => request !== name));
  }

  return (
    <Layout role="teacher" className="teacher-page">
      <div className="row">
        <section className="card">
          <h2>Add new project</h2>
          <div className="row">
            <div className="students-box">
              <form className="inline-form" onSubmit={addStudent}>
                <input type="text" placeholder="Add new student" required
                  value={studentName} onChange={(e) => setStudentName(e.target.value)} />
                <button type="submit">Add</button>
              </form>
              <ul className="list">
                {students.map((name, i) => <li key={i}>{name}</li>)}
              </ul>
            </div>

            <form onSubmit={(e) => e.preventDefault()}>
              <label htmlFor="project-name">Enter project name</label><br />
              <input type="text" id="project-name" placeholder="Enter text" required
                value={projectName} onChange={(e) => setProjectName(e.target.value)} />
              <br /><br />
              <button type="submit">Create project and groups</button>
            </form>
          </div>
        </section>

        <section className="card">
          <h2>Change requests</h2>
          <div>
            {requests.map((name) => (
              <div className="request" key={name}>
                <span>{name}</span>
                <button onClick={() => handleRequest(name)}>Deny</button>
                <button onClick={() => handleRequest(name)}>Approve</button>
              </div>
            ))}
          </div>
        </section>
      </div>

      <section className="card" style={{ marginTop: 24 }}>
        <h2>Groups</h2>
        <div className="groups">
          {groups.map((name) => <div className="group-tile" key={name}>{name}</div>)}
        </div>
      </section>
    </Layout>
  );
}
