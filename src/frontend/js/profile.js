//show a saved message. Nothing is stored yet.
document.getElementById("profile-form").addEventListener("submit", (event) => {
  event.preventDefault();
  document.getElementById("profile-status").textContent = "Saved.";
});
