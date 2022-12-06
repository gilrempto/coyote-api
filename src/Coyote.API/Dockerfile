#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Coyote.API/Coyote.API.csproj", "src/Coyote.API/"]
COPY ["src/Catalog/Coyote.Catalog/Coyote.Catalog.csproj", "src/Catalog/Coyote.Catalog/"]
COPY ["src/Catalog/Coyote.Catalog.MongoDB/Coyote.Catalog.MongoDB.csproj", "src/Catalog/Coyote.Catalog.MongoDB/"]
COPY ["src/Catalog/Coyote.Catalog.PostgreSQL/Coyote.Catalog.PostgreSQL.csproj", "src/Caalog/Coyote.Catalog.PostgreSQL/"]
RUN dotnet restore "src/Coyote.API/Coyote.API.csproj"
COPY . .
WORKDIR "/src/src/Coyote.API"
RUN dotnet build "Coyote.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Coyote.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Coyote.API.dll"]

# docker build -t coyote-api -f .\src\Coyote.API\Dockerfile .
# docker network create coyotenet
# docker run -it --rm --network coyotenet -p 8082:80 --name coyote-api coyote-api
# docker run -it --rm --network coyotenet -p 27017:27017 --name mongodb mongo:5.0.9-focal