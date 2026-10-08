//post new announcement at top of the list
document.getElementById("announcement-form").addEventListener("submit", (event) => {
  event.preventDefault();
  const input = document.getElementById("announcement-text");
  const item = document.createElement("div");
  item.className = "announcement";
  item.innerHTML = "<small>You</small>";
  item.append(input.value);
  document.getElementById("announcements").prepend(item);
  input.value = "";
});

//build the calenader for october
const calendar = document.getElementById("calendar");
const daySelect = document.getElementById("event-day");
const year = new Date().getFullYear();
const daysInOctober = 31;

//getDay() gives 0 for Sunday, so shift it to make monday the first column.
const firstWeekday = (new Date(year, 9, 1).getDay() + 6) % 7;

for (let i = 0; i < firstWeekday; i++) {
  const empty = document.createElement("div");
  empty.className = "day empty";
  calendar.append(empty);
}

for (let day = 1; day <= daysInOctober; day++) {
  const cell = document.createElement("div");
  cell.className = "day";
  cell.id = "day-" + day;
  cell.textContent = day;
  calendar.append(cell);

  const option = document.createElement("option");
  option.value = day;
  option.textContent = day + " October";
  daySelect.append(option);
}

//add event to the chosen day.
document.getElementById("event-form").addEventListener("submit", (event) => {
  event.preventDefault();
  const text = document.getElementById("event-text");
  const student = document.getElementById("event-student").value;
  const item = document.createElement("div");
  item.className = "event";
  item.textContent = student ? text.value + " (" + student + ")" : text.value;
  document.getElementById("day-" + daySelect.value).append(item);
  text.value = "";
});
