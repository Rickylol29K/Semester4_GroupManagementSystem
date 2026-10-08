//go to the student group page after logging in.
document.getElementById("login-form").addEventListener("submit", (event) => {
  event.preventDefault();
  window.location.href = "pages/student/group.html";
});
