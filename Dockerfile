# syntax=docker/dockerfile:1

# Build stage: restore with locked dependencies, then publish the API.
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy only the files that affect restore first so this layer is cached
# until a project file or lock file actually changes.
COPY global.json Directory.Build.props ./
COPY src/backend/Semester4_Project.API/Semester4_Project.API.csproj src/backend/Semester4_Project.API/packages.lock.json src/backend/Semester4_Project.API/
COPY src/backend/Semester4_Project.Business/Semester4_Project.Business.csproj src/backend/Semester4_Project.Business/packages.lock.json src/backend/Semester4_Project.Business/
COPY src/backend/Semester4_Project.DataAccess/Semester4_Project.DataAccess.csproj src/backend/Semester4_Project.DataAccess/packages.lock.json src/backend/Semester4_Project.DataAccess/

RUN dotnet restore src/backend/Semester4_Project.API/Semester4_Project.API.csproj --locked-mode

# Copy the remaining source and publish a framework-dependent build.
COPY src/ src/
RUN dotnet publish src/backend/Semester4_Project.API/Semester4_Project.API.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish

# Runtime stage: minimal ASP.NET runtime, no SDK.
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

# The aspnet images listen on 8080 by default and ship a non-root "app" user.
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
USER $APP_UID

ENTRYPOINT ["dotnet", "Semester4_Project.API.dll"]