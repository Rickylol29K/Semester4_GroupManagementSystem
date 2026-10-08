//add a student to the list.
document.getElementById("student-form").addEventListener("submit", (event) => {
  event.preventDefault();
  const input = document.getElementById("student-name");
  const item = document.createElement("li");
  item.textContent = input.value;
  document.getElementById("student-list").append(item);
  input.value = "";
});

document.getElementById("project-form").addEventListener("submit", (event) => {
  event.preventDefault();
});

//approving or denying a change request removes it from the list.
document.querySelectorAll(".request button").forEach((button) => {
  button.addEventListener("click", () => {
    button.parentElement.remove();
  });
});
