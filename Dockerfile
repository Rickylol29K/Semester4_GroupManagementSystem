# Builds the backend API into a Docker image.

# Step 1: build the API with the .NET SDK.
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the project files first and restore the packages.
# Docker remembers this step, so it only runs again when a package changes.
COPY global.json Directory.Build.props ./
COPY src/backend/Semester4_Project.API/Semester4_Project.API.csproj src/backend/Semester4_Project.API/packages.lock.json src/backend/Semester4_Project.API/
COPY src/backend/Semester4_Project.Business/Semester4_Project.Business.csproj src/backend/Semester4_Project.Business/packages.lock.json src/backend/Semester4_Project.Business/
COPY src/backend/Semester4_Project.DataAccess/Semester4_Project.DataAccess.csproj src/backend/Semester4_Project.DataAccess/packages.lock.json src/backend/Semester4_Project.DataAccess/
RUN dotnet restore src/backend/Semester4_Project.API/Semester4_Project.API.csproj --locked-mode

# Copy the rest of the code and publish the API.
COPY src/ src/
RUN dotnet publish src/backend/Semester4_Project.API/Semester4_Project.API.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish

# Step 2: run the API with only the smaller ASP.NET runtime.
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

# Run as the normal "app" user instead of root.
USER $APP_UID

ENTRYPOINT ["dotnet", "Semester4_Project.API.dll"]
