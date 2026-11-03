//all calls to the backend

//sends json and returns the answer
async function send(method, url, body) {
  const response = await fetch(url, {
    method,
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(body),
  });
  const data = await response.json().catch(() => null);

  if (!response.ok) {
    throw new Error(errorMessage(data));
  }
  return data;
}

//turns the backend error into a message
function errorMessage(data) {
  if (data?.error) return data.error;
  if (data?.errors) return Object.values(data.errors).flat().join(" ");
  return "Something went wrong.";
}

export function register(email, password, role) {
  return send("POST", "/api/auth/register", { email, password, role });
}

export function login(email, password, staySignedIn) {
  return send("POST", "/api/auth/login", { email, password, staySignedIn });
}

export function logout() {
  return fetch("/api/auth/logout", { method: "POST" });
}

//null if nobody is logged in
export async function getCurrentUser() {
  const response = await fetch("/api/auth/me");
  return response.ok ? response.json() : null;
}

export async function getStudents() {
  const response = await fetch("/api/teacher/students");
  return response.ok ? response.json() : [];
}

//empty if the student never saved one
export async function getProfile() {
  const response = await fetch("/api/student/profile");
  if (!response.ok) throw new Error("Could not load your profile.");
  return response.json();
}

export function saveProfile(profile) {
  return send("PUT", "/api/student/profile", profile);
}

//first page after logging in
export function homePage(user) {
  return user.role === "Teacher" ? "/teacher/groups" : "/student/group";
}
