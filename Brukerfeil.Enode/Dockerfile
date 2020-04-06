FROM  mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app
ENV HOME=/root
ENV ASPNETCORE_ENVIRONMENT Development

COPY Brukerfeil.Enode.sln ./
COPY Brukerfeil.Enode.API/Brukerfeil.Enode.API.csproj ./Brukerfeil.Enode.API/
COPY Brukerfeil.Enode.Common/Brukerfeil.Enode.Common.csproj ./Brukerfeil.Enode.Common/
COPY Brukerfeil.Enode.Repositories/Brukerfeil.Enode.Repositories.csproj ./Brukerfeil.Enode.Repositories/
COPY Brukerfeil.Enode.Services/Brukerfeil.Enode.Services.csproj ./Brukerfeil.Enode.Services/
COPY Brukerfeil.Enode.Schemas/Brukerfeil.Enode.Schemas.csproj ./Brukerfeil.Enode.Schemas/
COPY Brukerfeil.Enode.Tests/Brukerfeil.Enode.Tests.csproj ./Brukerfeil.Enode.Tests/
COPY nuget.config ./
COPY Brukerfeil.Enode.API/appsettings.json ./appsettings.json

RUN dotnet restore Brukerfeil.Enode.API/Brukerfeil.Enode.API.csproj

COPY . .

RUN dotnet publish Brukerfeil.Enode.API/Brukerfeil.Enode.API.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app/out ./
ENV ASPNETCORE_URLS="http://+:53980"
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENTRYPOINT ["dotnet", "Brukerfeil.Enode.API.dll", "--server.urls", "http://+:53980"]
