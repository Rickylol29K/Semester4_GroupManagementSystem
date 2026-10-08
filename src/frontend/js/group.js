//accept the group or ask teacher for change
const status = document.getElementById("group-status");

document.getElementById("accept-group").addEventListener("click", () => {
  status.textContent = "You accepted this group.";
});

document.getElementById("request-change").addEventListener("click", () => {
  status.textContent = "Your change request was sent to the teacher.";
});
