#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt update -y
RUN apt install curl -y

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Pharmm.API.csproj", ""]
RUN dotnet restore -s "http://174.138.22.139:5555/v3/index.json" -s "https://api.nuget.org/v3/index.json" "./Pharmm.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Pharmm.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pharmm.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pharmm.API.dll"]