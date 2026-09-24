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
.github/workflows/
  ci.yml
  cd.yml
Dockerfile
```

The three production projects follow **API → Business → DataAccess**. The browser
frontend will communicate with the API over HTTP.

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

### Run in a container

A multi-stage `Dockerfile` builds and publishes the API. It restores with locked
dependencies, publishes a Release build, and runs on the minimal ASP.NET runtime
image as a non-root user on port `8080`.

```sh
docker build -t semester4-project-api .
docker run --rm -p 8085:8080 semester4-project-api
# health endpoint is then http://localhost:8085/health
```

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

Branch protection is a separate repository setting; it is not enabled by this
scaffold.

Workflow reference: [GitHub's .NET build and test guide](https://docs.github.com/en/actions/tutorials/build-and-test-code/net).

## Continuous delivery

The **CD** workflow (`.github/workflows/cd.yml`) builds the API container image and
publishes it to the GitHub Container Registry (GHCR). It runs when `main` is
updated, when a `v*.*.*` tag is pushed, or on manual dispatch. It:

1. Restores, builds, and tests the solution — a failing test stops the publish.
2. Builds the image from the `Dockerfile` and pushes it to
   `ghcr.io/<owner>/<repo>` using the built-in `GITHUB_TOKEN` (no external
   secrets required).
3. Tags the image with the branch name and short commit SHA, adds `latest` on the
   default branch, and adds semantic-version tags (for example `1.2.3` and `1.2`)
   when a `v*.*.*` tag is pushed.

To cut a versioned release, push a tag:

```sh
git tag v0.1.0
git push origin v0.1.0
```

The published image is a runnable artifact. Pointing it at a specific host
(for example Azure App Service, a container host, or Kubernetes) is a deployment
step to add once a hosting target is chosen — extend the CD workflow with a deploy
job that pulls the image and releases it there.
