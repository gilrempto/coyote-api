# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY "src/Coyote.API/Coyote.API.csproj" "./Coyote.API/Coyote.API.csproj"
COPY "src/Catalog/Coyote.Catalog/Coyote.Catalog.csproj" "./Catalog/Coyote.Catalog/Coyote.Catalog.csproj"
COPY "src/Catalog/Coyote.Catalog.MongoDB/Coyote.Catalog.MongoDB.csproj" "./Catalog/Coyote.Catalog.MongoDB/Coyote.Catalog.MongoDB.csproj"
#COPY "src/Catalog/Coyote.Catalog.PostgreSQL/Coyote.Catalog.PostgreSQL.csproj" "./Catalog/Coyote.Catalog.PostgreSQL/Coyote.Catalog.PostgreSQL.csproj"
RUN dotnet restore "./Coyote.API/Coyote.API.csproj"

# copy everything else and build app
COPY src/. ./
RUN dotnet publish "./Coyote.API/Coyote.API.csproj" -c release -o /publish --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /publish ./
ENTRYPOINT ["dotnet", "Coyote.API.dll"]

# docker build -t coyote-api -f .\src\Coyote.API\Dockerfile .
# docker network create coyote net
# docker run -it --rm --network coyotenet -p 8082:80 --name coyote-api coyote-api
# docker run -it --rm --network coyotenet -p 27017:27017 --name mongodb mongo:5.0.9-focal
