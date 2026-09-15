# Three-layer architecture

The backend consists of three separate .NET 10 projects. Their direct project
references follow the traditional three-layer dependency direction.

| Layer | Project | Responsibility |
| --- | --- | --- |
| Presentation/API | `Semester4_Project.API` | HTTP endpoints, request/response handling, and calling business operations. |
| Business logic | `Semester4_Project.Business` | Matching rules, profile validation, joining projects, and team collaboration rules. |
| Data access | `Semester4_Project.DataAccess` | Loading and saving students, profiles, projects, teams, and tasks. |

The separate `src/frontend` folder is for the browser interface. It communicates
with the API over HTTP.

## Dependency direction

```text
Frontend → API → Business → DataAccess → Database
```

The API project references Business, and Business references DataAccess.
DataAccess has no project references. Transitive project references are disabled
in `Directory.Build.props`, so API code cannot access DataAccess types merely
because Business references DataAccess. API request handlers should call business
operations rather than querying the database directly.

Registration of concrete services can be handled centrally during application
startup when features are implemented. Keep matching rules and database queries
out of frontend components and API controllers.

For example, a request to generate teams should reach an API endpoint, pass to a
Business service that applies matching rules, and use DataAccess to retrieve
profiles and save the resulting teams. These features have not been implemented.

This arrangement follows the [traditional layered architecture described by Microsoft](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#traditional-n-layer-architecture-applications).

## Tests

Reserve `tests/unit` for tests of individual business rules. The integration test
project under `tests/integration` starts the real API pipeline in a test host and
checks that `/health` responds successfully. It does not exercise business rules,
a database, or external services, which have not yet been implemented.
