//announcements and calendar, still example data

import { useState } from "react";
import Layout from "../../components/Layout.jsx";

const dayNames = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];
const students = ["Student 1", "Student 2", "Student 3", "Student 4", "Student 5", "Student 6"];
const daysInOctober = 31;

//make monday the first day
const firstWeekday = (new Date(new Date().getFullYear(), 9, 1).getDay() + 6) % 7;
const days = Array.from({ length: daysInOctober }, (_, i) => i + 1);

export default function Dashboard() {
  const [announcements, setAnnouncements] = useState([
    { author: "Student 6", text: "Lorem ipsum dolor sit amet, consectetur adipiscing elit." },
    { author: "Student 2", text: "Lorem ipsum dolor sit amet, consectetur adipiscing elit." },
  ]);
  const [announcementText, setAnnouncementText] = useState("");

  //events per day
  const [events, setEvents] = useState({});
  const [eventText, setEventText] = useState("");
  const [eventDay, setEventDay] = useState("");
  const [eventStudent, setEventStudent] = useState("");

  //new announcement goes on top
  function postAnnouncement(event) {
    event.preventDefault();
    setAnnouncements([{ author: "You", text: announcementText }, ...announcements]);
    setAnnouncementText("");
  }

  //add event to the chosen day
  function createEvent(event) {
    event.preventDefault();
    const label = eventStudent ? eventText + " (" + eventStudent + ")" : eventText;
    setEvents({ ...events, [eventDay]: [...(events[eventDay] || []), label] });
    setEventText("");
  }

  return (
    <Layout role="Student">
      <div className="row">
        <section className="card">
          <h2>Announcements</h2>
          <div>
            {announcements.map((item, i) => (
              <div className="announcement" key={i}>
                <small>{item.author}</small>
                {item.text}
              </div>
            ))}
          </div>
          <form className="inline-form" onSubmit={postAnnouncement}>
            <input type="text" placeholder="Enter text" required
              value={announcementText} onChange={(e) => setAnnouncementText(e.target.value)} />
            <button type="submit">Post</button>
          </form>
        </section>

        <section>
          <h2 className="calendar-title">October</h2>
          <div className="calendar">
            {dayNames.map((name) => <div className="day-name" key={name}>{name}</div>)}
            {Array.from({ length: firstWeekday }, (_, i) => <div className="day empty" key={"empty-" + i}></div>)}
            {days.map((day) => (
              <div className="day" key={day}>
                {day}
                {(events[day] || []).map((label, i) => <div className="event" key={i}>{label}</div>)}
              </div>
            ))}
          </div>

          <form className="inline-form" onSubmit={createEvent}>
            <input type="text" placeholder="Enter text" required
              value={eventText} onChange={(e) => setEventText(e.target.value)} />
            <select required value={eventDay} onChange={(e) => setEventDay(e.target.value)}>
              <option value="">Select day</option>
              {days.map((day) => <option key={day} value={day}>{day} October</option>)}
            </select>
            <select value={eventStudent} onChange={(e) => setEventStudent(e.target.value)}>
              <option value="">Assign student</option>
              {students.map((name) => <option key={name}>{name}</option>)}
            </select>
            <button type="submit">Create</button>
          </form>
        </section>
      </div>
    </Layout>
  );
}
