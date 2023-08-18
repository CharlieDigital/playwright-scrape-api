# (1) The build environment
FROM mcr.microsoft.com/dotnet/sdk:6.0-jammy as build
WORKDIR /app

# (2) Copy the .csproj and restore; this will cache these layers so they are not run if no changes.
COPY ./playwright-scrape.csproj ./playwright-scrape.csproj
RUN dotnet restore

# (3) Copy the application files and build.
COPY ./Program.cs ./Program.cs
RUN dotnet publish ./playwright-scrape.csproj -o /app/published-app --configuration Release

# (4) The dotnet tagged Playwright environment includes .NET
FROM mcr.microsoft.com/playwright/dotnet:v1.37.0-jammy as playwright
WORKDIR /app
COPY --from=build /app/published-app /app

ENV IS_CONTAINER=true

# (5) Start our app!
ENTRYPOINT [ "dotnet", "/app/playwright-scrape.dll" ]