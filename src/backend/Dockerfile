
##
##	ATTENTION!
##  If change port, the frontend may crash, it is mapped to consume backend at port 4999
##

# Args with their default values
ARG PORT=4999
# possible values for environment: Development, Test
ARG ASPNETCORE_ENVIRONMENT="Test"
ARG ASPNETCORE_URLS=http://+:$PORT


# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy the solution file and restore dependencies
COPY ./Rotas.Api/*.csproj ./Rotas.Api/
COPY ./Rotas.Domain/*.csproj ./Rotas.Domain/
COPY ./Rotas.Application/*.csproj ./Rotas.Application/
COPY ./Rotas.DataAccess.EF/*.csproj ./Rotas.DataAccess.EF/
RUN dotnet restore ./Rotas.Api/Rotas.Api.csproj

# Copy the entire source code and build the application
COPY ./Rotas.Api ./Rotas.Api
COPY ./Rotas.Domain ./Rotas.Domain
COPY ./Rotas.Application ./Rotas.Application
COPY ./Rotas.DataAccess.EF ./Rotas.DataAccess.EF

# publish after migrations
RUN dotnet publish ./Rotas.Api/Rotas.Api.csproj -c Release -o /publish

# Use the runtime image to run the application
# the runtime uses SDK because the dotnet tool install and EF migrations
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy the published output
COPY --from=build /publish .

# refer locally the global var, kinda like a python variable scope
ARG ASPNETCORE_ENVIRONMENT
ARG ASPNETCORE_URLS

# Set the environment variable for the runtime
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT
ENV ASPNETCORE_URLS=$ASPNETCORE_URLS

# Dynamically copy the correct appsettings file based on the environment
COPY ./Rotas.Api/appsettings.${ASPNETCORE_ENVIRONMENT}.json ./appsettings.json

# Expose the DEFAULT port the application listens on
EXPOSE 8080
EXPOSE 4999

ENTRYPOINT ["dotnet", "Rotas.Api.dll"]
