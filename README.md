# Semester 4 Group Management System

A student team formation and collaboration platform for the Fontys full stack
development semester. The planned platform matches students using their skills,
interests, learning goals, working preferences, and availability, then provides
teams with a project workspace.

## Current scope

This repository contains a minimal, buildable .NET 10 scaffold with three backend
layers, one startup integration test, and GitHub Actions CI. Student accounts,
matching, project management, database access, and frontend features have not been
implemented. The frontend framework and database remain to be selected.

## Structure

```text
Semester4_Project.sln
src/
  backend/
    Semester4_Project.API/
    Semester4_Project.Business/
    Semester4_Project.DataAccess/
  frontend/
tests/
  unit/
  integration/
    Semester4_Project.IntegrationTests/
docs/
  architecture.md
.github/workflows/ci.yml
```

The three production projects follow **API → Business → DataAccess**. The browser
frontend will communicate with the API over HTTP. See [architecture](docs/architecture.md).

## Build and run

Install the .NET SDK specified in `global.json` and open `Semester4_Project.sln`
in Rider. Select the API project and its `http` launch profile to run it.

```sh
dotnet restore Semester4_Project.sln --locked-mode
dotnet build Semester4_Project.sln --configuration Release --no-restore
dotnet test Semester4_Project.sln --configuration Release --no-build
dotnet run --project src/backend/Semester4_Project.API --launch-profile http
```

The startup health endpoint is `http://localhost:5100/health`. It returns
`Healthy` when the application starts. It does not check a database or external
service. No other application endpoints exist yet.

## Continuous integration

The **CI** workflow runs on pushes, pull requests, and manual dispatches. It:

1. Installs the SDK specified in `global.json`.
2. Restores dependencies using committed package lock files.
3. Checks formatting with `dotnet format`.
4. Builds all projects in Release mode.
5. Runs the integration test and uploads its results for seven days.

Run `dotnet format Semester4_Project.sln --verify-no-changes --no-restore` locally
to check formatting. After deliberately updating a package, run `dotnet restore`
and commit the updated `packages.lock.json` files with the project changes.

The current test verifies that the API starts and serves its health endpoint.
Add business rule tests under `tests/unit` and integration tests as features are
implemented. Add frontend CI after selecting and creating the frontend project.

CI does not deploy the application. Branch protection is a separate repository
setting; it is not enabled by this scaffold.

Workflow reference: [GitHub's .NET build and test guide](https://docs.github.com/en/actions/tutorials/build-and-test-code/net).
