# Group Management

Students and teachers make an account. Students fill in a profile so they can be put in a
fitting project group. Teachers see the students and manage the groups.

## How the project is organized

```
src/
  backend/
    Semester4_Project.API/          Layer 1: receives requests from the frontend
      Controllers/                    one controller per part of the app (auth, profile, teacher)
      Models/                         the data the frontend sends (login, register)
      Program.cs                      start point: sets everything up and starts the server
    Semester4_Project.Business/     Layer 2: the rules of the app
      Services/                       AccountService (accounts), ProfileService (profiles)
      Models/                         the data the services give back
    Semester4_Project.DataAccess/   Layer 3: talks to the database
      Entities/                       one class per database table
      Repositories/                   reads and saves the tables
      Migrations/                     generated code that creates the tables
  frontend/                         React app
    src/pages/                        one file per page (student/ and teacher/ pages)
    src/components/                   pieces used on many pages (sidebar, role check)
    src/api.js                        all calls to the backend
tests/
  Semester4_Project.Tests/          unit tests for the services, using an in-memory database
```

A request goes: **frontend → Controller → Service → Repository → database**.

## How to run it

1. Start the database: `docker compose up -d`
2. Start the backend: `dotnet run --project src/backend/Semester4_Project.API`
3. Start the frontend: `cd src/frontend`, then `npm install` and `npm run dev`
4. Open the address Vite shows (usually http://localhost:5173)

Run the tests with `dotnet test`.

## Pipeline

`.github/workflows/pipeline.yml` builds and tests everything on every push.
On `main` it also builds the `Dockerfile` and puts the backend image on GitHub.
