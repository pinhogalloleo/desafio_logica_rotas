
# Args with their default values
ARG PORT=8080
ARG ASPNETCORE_ENVIRONMENT="Development"
ARG ASPNETCORE_URLS=http://+:$PORT

# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy the solution file and restore dependencies
COPY ./Rotas.Api/*.csproj ./Rotas.Api/
COPY ./Rotas.Domain/*.csproj ./Rotas.Domain/
COPY ./Rotas.Application/*.csproj ./Rotas.Application/
COPY ./Rotas.DataAccess.FileDataAccess/*.csproj ./Rotas.DataAccess.FileDataAccess/
RUN dotnet restore ./Rotas.Api/Rotas.Api.csproj

# Copy the entire source code and build the application
COPY ./Rotas.Api ./Rotas.Api
COPY ./Rotas.Domain ./Rotas.Domain
COPY ./Rotas.Application ./Rotas.Application
COPY ./Rotas.DataAccess.FileDataAccess ./Rotas.DataAccess.FileDataAccess
RUN dotnet publish ./Rotas.Api/Rotas.Api.csproj -c Release -o /publish

# Use the runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy the published output
COPY --from=build /publish .

# refer locally the global var, kinda like a python variable scope
ARG PORT
ARG ASPNETCORE_ENVIRONMENT
ARG ASPNETCORE_URLS

# Set the environment variable for the runtime
ENV PORT=$PORT
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT
ENV ASPNETCORE_URLS=$ASPNETCORE_URLS

# Dynamically copy the correct appsettings file based on the environment
COPY ./Rotas.Api/appsettings.${ASPNETCORE_ENVIRONMENT}.json ./appsettings.${ASPNETCORE_ENVIRONMENT}.json

# Expose the DEFAULT port the application listens on
EXPOSE 4999

# Set the entry point for the container
ENTRYPOINT ["dotnet", "Rotas.Api.dll"]
