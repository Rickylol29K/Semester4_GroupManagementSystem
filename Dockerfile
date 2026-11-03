# Builds the backend API into a Docker image.

# Step 1: build the API with the .NET SDK.
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY src/backend/ ./
RUN dotnet publish Semester4_Project.API -c Release -o /app

# Step 2: run the API with only the smaller ASP.NET runtime.
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app ./
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Semester4_Project.API.dll"]
