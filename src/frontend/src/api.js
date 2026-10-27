//all calls to the backend in one place (register, login, logout, who is logged in).

async function post(url, body) {
  const response = await fetch(url, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(body),
  });
  const data = await response.json().catch(() => null);

  if (!response.ok) {
    throw new Error(errorMessage(data));
  }
  return data;
}

//the backend sends either { error } or validation errors { errors: { Field: [...] } }.
function errorMessage(data) {
  if (data?.error) return data.error;
  if (data?.errors) return Object.values(data.errors).flat().join(" ");
  return "Something went wrong.";
}

export function register(email, password, role) {
  return post("/api/auth/register", { email, password, role });
}

export function login(email, password, staySignedIn) {
  return post("/api/auth/login", { email, password, staySignedIn });
}

export function logout() {
  return fetch("/api/auth/logout", { method: "POST" });
}

//returns the logged in user, or null if nobody is logged in.
export async function getCurrentUser() {
  const response = await fetch("/api/auth/me");
  return response.ok ? response.json() : null;
}

export async function getStudents() {
  const response = await fetch("/api/teacher/students");
  return response.ok ? response.json() : [];
}

//the first page a user sees after logging in.
export function homePage(user) {
  return user.role === "Teacher" ? "/teacher/groups" : "/student/group";
}
